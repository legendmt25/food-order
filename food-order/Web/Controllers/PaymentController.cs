using Microsoft.AspNetCore.Mvc;
using Service;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(OperationId = "getToken")]
    public Task<string> generateToken()
    {
        return paymentService.generateToken();
    }
}