using System.Text;
using Models;
using Repository;
namespace Service;

public class OrderService
{
    private readonly ShoppingCartRepository shoppingCartRepository;
    private readonly OrderRepository orderRepository;
    private readonly ShoppingCartService shoppingCartService;
    private readonly UserService userService;
    private readonly MailService mailService;


    public OrderService(ShoppingCartService shoppingCartService, UserService userService, OrderRepository orderRepository, ShoppingCartRepository shoppingCartRepository, MailService mailService)
    {
        this.shoppingCartService = shoppingCartService;
        this.userService = userService;
        this.orderRepository = orderRepository;
        this.shoppingCartRepository = shoppingCartRepository;
        this.mailService = mailService;
    }

    private  decimal totalPrice(Order order)
    {
        return order.foodOrderEntry.items.Sum(item =>
        {
            int sizePriceFactor = 1;
            if (item.size == FoodSize.MIDDLE)
            {
                sizePriceFactor = 2;
            }
            else if (item.size == FoodSize.BIG)
            {
                sizePriceFactor = 3;
            }
            return sizePriceFactor * item.quantity * (item.accessories.Sum(accessory => accessory.price) + item.food.price);
        });
    }

    private string generateHtml(Order order)
    {
        StringBuilder html = new StringBuilder()
            .Append($"<div>")
            .Append($"<h2>Food order number {order.id}</h2>")
            .Append($"<hr />")
            .Append($"<table style=\"width: 100%\">")
            .Append($"<thead>")
            .Append($"<tr>")
            .Append($"<td>Food</td>")
            .Append($"<td>Category</td>")
            .Append($"<td>Quantity</td>")
            .Append($"<td>Price</td>")
            .Append($"</tr>")
            .Append($"</thead>")
            .Append($"<tbody>");

        foreach (var item in order.foodOrderEntry.items)
        {
            int sizePriceFactor = 1;
            if (item.size == FoodSize.MIDDLE)
            {
                sizePriceFactor = 2;
            }
            else if (item.size == FoodSize.BIG)
            {
                sizePriceFactor = 3;
            }

            html.Append($"<tr>")
                .Append($"<td>{item.food.name}</td>")
                .Append($"<td>{item.food.category}</td>")
                .Append($"<td>{item.quantity}</td>")
                .Append($"<td>{sizePriceFactor * item.quantity * (item.food.price + item.accessories.Sum(accessory => accessory.price))}</td>")
                .Append($"</tr>");
        }

        html.Append($"</tbody>")
                .Append($"<tfoot>")
                .Append($"<tr>")
                .Append($"<td colspan=\"3\">TotalPrice</td>")
                .Append($"<td>{this.totalPrice(order)}</td>")
                .Append($"</tr>")
                .Append($"</tfoot>")
                .Append($"</table>")
                .Append($"<p>Your order will be sent within 40 minutes, Thank you for trusting us</p>")
                .Append($"</div>");
        return html.ToString();
    }

    public async Task<ICollection<Order>> findByUsername(string username)
    {
        return await orderRepository.findByUsername(username);
    }

    public async Task makeOrder(string username, UserAddress address)
    {
        ShoppingCart cart = await shoppingCartService.findByUsername(username);
        AppUser user = await userService.findByUsername(username);
        Order order = new Order(cart.foodCartEntry, user, address);
        Order saved = await orderRepository.save(order);
        await mailService.send(user.Email, "Food order ready", generateHtml(saved));
        await shoppingCartRepository.delete(cart.id.Value);
    }
}