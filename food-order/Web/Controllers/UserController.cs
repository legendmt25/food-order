using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

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
    public async Task<IEnumerable<AppUser>> users()
    {
        return await userService.findAll();
    }

    [HttpGet("{id}")]
    public async Task<AppUser> user(int id)
    {
        return await userService.findById(id);
    }

    [HttpPost]
    public async Task save(AppUser user)
    {
        await userService.save(user);
    }

    [HttpDelete("{id}")]
    public async Task delete(int id)
    {
        await userService.deleteById(id);
    }
}