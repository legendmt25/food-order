using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController
{
    private readonly OrderService orderService;
    public OrderController(OrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task makeOrder()
    {
        // TODO: Get user from jwt token
        await orderService.makeOrder("");
    }

}