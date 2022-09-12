using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers;

[ApiController]
[EnableCors]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{UserRole.ADMIN}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<IdentityRole<int>> roleManager;
    private readonly UserService userService;
    private readonly FoodService foodService;

    public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager, UserService userService, FoodService foodService)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.userService = userService;
        this.foodService = foodService;
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
    }

    [HttpPost]
    [Route("create-admin")]
    [SwaggerOperation(OperationId = "createAdmin")]
    public async Task createAdmin(RegisterDto input)
    {
        IdentityResult identityResult = await userManager.CreateAsync(new AppUser(input.firstName, input.lastName, input.email, input.username), input.password);
        if (!identityResult.Succeeded)
        {
            throw new HttpRequestException("Could not register user");
        }
        AppUser user = await userManager.FindByNameAsync(input.username);
        if (!await roleManager.RoleExistsAsync(UserRole.ADMIN))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(UserRole.ADMIN));
        }
        await userManager.AddToRoleAsync(user, UserRole.ADMIN);
    }

    [HttpPost]
    [Route("import-users")]
    [SwaggerOperation(OperationId = "importUsers")]
    public async Task importUsers(IFormFile file)
    {
        ExcelFile workbook = ExcelFile.Load(file.OpenReadStream());
        ExcelWorksheet worksheet = workbook.Worksheets.Where(worksheet => worksheet.Name.Equals("users")).First();
        foreach (ExcelRow row in worksheet.Rows)
        {
            string firstName = row.Cells[0].Value.ToString();
            string lastName = row.Cells[1].Value.ToString();
            string email = row.Cells[2].Value.ToString();
            string username = row.Cells[3].Value.ToString();
            string password = row.Cells[4].Value.ToString();
            string[] roles = row.Cells[5].Value.ToString().Split(',');

            IdentityResult identityResult = await userManager.CreateAsync(new AppUser(firstName, lastName, email, username), password);
            if (!identityResult.Succeeded)
            {
                throw new HttpRequestException("Could not register user");
            }
            AppUser user = await userManager.FindByNameAsync(username);
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
            await userManager.AddToRolesAsync(user, roles);
        }
    }


    [HttpPost]
    [Route("import-foods")]
    [SwaggerOperation(OperationId = "importFoods")]
    public async Task importFoods(IFormFile file)
    {
        ExcelFile workbook = ExcelFile.Load(file.OpenReadStream());
        ExcelWorksheet worksheet = workbook.Worksheets.Where(worksheet => worksheet.Name.Equals("foods")).First();
        foreach (ExcelRow row in worksheet.Rows)
        {
            try
            {
                ExcelPicture picture = worksheet.Pictures.FirstOrDefault(picture => picture.Metadata.Name.Equals($"Food {row.Index + 1}"));
                ImageData image = new ImageData(picture.PictureStream.ToArray(), $"image/{picture.PictureFormat.ToString().ToLower()}");
                string name = row.Cells[0].Value.ToString();
                string description = row.Cells[1].Value.ToString();
                FoodCategory category;
                Enum.TryParse<FoodCategory>(row.Cells[2].Value.ToString(), out category);
                decimal price = Convert.ToDecimal(row.Cells[3].Value.ToString());

                List<ExcelCell> accessoryCells = row.Cells.ToList()
                    .GetRange(4, row.Cells.LastColumnIndex - 4)
                    .TakeWhile((cell) => cell.Value != null)
                    .ToList();


                List<FoodAccessory> accessories = new List<FoodAccessory>();
                for (int i = 0; i + 1 < accessoryCells.Count(); i += 2)
                {
                    string accessoryName = accessoryCells[i].Value.ToString();
                    decimal accessoryPrice = Convert.ToDecimal(accessoryCells[i + 1].Value.ToString());
                    accessories.Add(new FoodAccessory(accessoryName, accessoryPrice));
                }
                Food food = new Food(name, description, category, accessories, price, image);
                await foodService.save(food);
            }
            catch
            {
                break;
            }
        }
    }

}