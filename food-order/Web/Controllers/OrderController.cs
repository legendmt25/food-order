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
public class OrderController : ControllerBase
{
    private readonly OrderService orderService;
    private readonly PaymentService paymentService;
    private readonly ShoppingCartService shoppingCartService;
    private readonly UserAddressService userAddressService;
    public OrderController(OrderService orderService, PaymentService paymentService, ShoppingCartService shoppingCartService, UserAddressService userAddressService)
    {
        this.orderService = orderService;
        this.paymentService = paymentService;
        this.shoppingCartService = shoppingCartService;
        this.userAddressService = userAddressService;
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "getOrders")]
    public async Task<ICollection<Order>> GetOrders()
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        return await orderService.findByUsername(User.Identity.Name);
    }

    [HttpPost]
    [SwaggerOperation(OperationId = "makeOrder")]
    public async Task makeOrder(TransactionDto transaction, [FromQuery] int addressId)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        decimal amount = await this.shoppingCartService.getTotalPriceByUsername(User.Identity.Name);
        bool paymentResult = await paymentService.makePayment(transaction, amount);
        if (!paymentResult)
        {
            throw new HttpRequestException("Payment unsuccessfull");
        }
        UserAddress address = await this.userAddressService.findById(addressId);
        await orderService.makeOrder(User.Identity.Name, address);
    }

}