using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(OperationId = "getShoppingCart")]
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
    [SwaggerOperation(OperationId = "addItem")]
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
    [SwaggerOperation(OperationId = "removeItem")]
    public async Task removeItem([FromRoute] int id)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        await shoppingCartService.removeItem(User.Identity.Name, id);
    }

}