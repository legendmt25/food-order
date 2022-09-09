using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{UserRole.ADMIN}")]
public class UserController
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "getUserEntries")]
    public async Task<IEnumerable<AppUser>> users()
    {
        return await userService.findAll();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "getUserEntry")]
    public async Task<AppUser> user(int id)
    {
        return await userService.findById(id);
    }

    [HttpPost]
    [SwaggerOperation(OperationId = "saveUserEntry")]
    public async Task save(AppUser user)
    {
        await userService.save(user);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "deleteUserEntry")]
    public async Task delete(int id)
    {
        await userService.deleteById(id);
    }
}