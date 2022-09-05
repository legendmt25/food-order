using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ShoppingCartController : ControllerBase
{
    private readonly ShoppingCartService shoppingCartService;
    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        this.shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public async Task<ShoppingCart> shoppingCart()
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        return await shoppingCartService.findByUsername(User.Identity.Name);
    }

    [HttpPost]
    [Route("add-item")]
    public async Task addItem(FoodAddItemDto foodAddItemDto)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        await shoppingCartService.addItem(User.Identity.Name, foodAddItemDto);
    }

    [HttpGet]
    [Route("remove-item/{id}")]
    public async Task removeItem([FromRoute] int id)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        await shoppingCartService.removeItem(User.Identity.Name, id);
    }

}