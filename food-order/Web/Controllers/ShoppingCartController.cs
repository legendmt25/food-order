using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ShoppingCartController
{
    private readonly ShoppingCartService shoppingCartService;
    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        this.shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public async Task<ShoppingCart> shoppingCart()
    {
        // TODO: Get user from jwt token
        return await shoppingCartService.findByUsername("");
    }

    [HttpPost]
    [Route("add-item")]
    public async Task addItem(FoodAddItemDto foodAddItemDto)
    {
        // TODO: Get user from jwt token
        await shoppingCartService.addItem("", foodAddItemDto);
    }

    [HttpGet]
    [Route("remove-item/{id}")]
    public async Task removeItem([FromRoute] int id)
    {
        // TODO: Get user from jwt token
        await shoppingCartService.removeItem("", id);
    }

}