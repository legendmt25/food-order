using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly UserManager<AppUser> userManager;
    private readonly UserService userService;
    private readonly TokenService tokenService;
    public AuthController(UserManager<AppUser> userManager, UserService userService, TokenService tokenService)
    {
        this.userManager = userManager;
        this.userService = userService;
        this.tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task register(RegisterDto input)
    {
        await userManager.CreateAsync(new AppUser(input.firstName, input.lastName, input.email, input.username), input.password);
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