using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
    private readonly OrderService orderService;
    public OrderController(OrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task makeOrder()
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        await orderService.makeOrder(User.Identity.Name);
    }

}