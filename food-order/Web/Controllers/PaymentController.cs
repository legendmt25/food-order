using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController
{
    private readonly PaymentService paymentService;

    public PaymentController(PaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    [HttpGet]
    [Route("token")]
    public Task<string> generateToken() {
        return paymentService.generateToken();
    }
}