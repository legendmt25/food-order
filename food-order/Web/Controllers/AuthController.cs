using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
[EnableCors]
[Route("[controller]")]
public class AuthController
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<IdentityRole<int>> roleManager;
    private readonly UserService userService;
    private readonly TokenService tokenService;
    public AuthController(UserManager<AppUser> userManager, UserService userService, TokenService tokenService, RoleManager<IdentityRole<int>> roleManager)
    {
        this.userManager = userManager;
        this.userService = userService;
        this.tokenService = tokenService;
        this.roleManager = roleManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task register(RegisterDto input)
    {
        IdentityResult identityResult = await userManager.CreateAsync(new AppUser(input.firstName, input.lastName, input.email, input.username), input.password);
        if (!identityResult.Succeeded)
        {
            throw new HttpRequestException("Could not register user");
        }
        AppUser user = await userManager.FindByNameAsync(input.username);
        if (!await roleManager.RoleExistsAsync(UserRole.USER))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(UserRole.USER));
        }
        await userManager.AddToRoleAsync(user, UserRole.USER);
    }

    [HttpPost]
    public async Task<string> login(LoginDto input)
    {
        AppUser user = await userService.findByUsername(input.username);
        bool passwordMatch = await this.userManager.CheckPasswordAsync(user, input.password);
        if (!passwordMatch)
        {
            throw new HttpRequestException("Invalid username and password");
        }
        return tokenService.createToken(user);
    }
}