using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
    private readonly OrderService orderService;
    private readonly PaymentService paymentService;
    public OrderController(OrderService orderService, PaymentService paymentService)
    {
        this.orderService = orderService;
        this.paymentService = paymentService;
    }

    [HttpPost]
    public async Task makeOrder(TransactionDto transaction)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        bool paymentResult = await paymentService.makePayment(transaction);
        if(paymentResult) {
            throw new HttpRequestException("Payment unsuccessfull");
        }
        await orderService.makeOrder(User.Identity.Name);
    }

}