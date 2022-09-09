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
            html.Append($"<tr>")
                .Append($"<td>{item.food.id}</td>")
                .Append($"<td>${item.food.category}</td>")
                .Append($"<td>{item.quantity}</td>")
                .Append($"<td>{item.quantity * item.food.price}</td>")
                .Append($"</tr>");
        }

        html.Append($"</tbody>")
            .Append($"<tfoot>")
            .Append($"<tr>")
            .Append($"<td colspan=\"3\">TotalPrice</td>")
            .Append($"<td>{order.foodOrderEntry.items.Sum(item => item.quantity * item.food.price)}</td>")
            .Append($"</tr>")
            .Append($"</tfoot>")
            .Append($"</table>")
            .Append($"<p>Your order will be sent within 40 minutes, Thank you for trusting us</p>")
            .Append($"</div>");
        return html.ToString();
    }

    public async Task<ICollection<Order>> findByUsername(string username) {
        return await orderRepository.findByUsername(username);
    }

    public async Task makeOrder(string username)
    {
        ShoppingCart cart = await shoppingCartService.findByUsername(username);
        AppUser user = await userService.findByUsername(username);
        Order order = new Order(cart.foodCartEntry, user);
        Order saved = await orderRepository.save(order);
        await mailService.send(user.Email, "Food order ready", generateHtml(saved));
        await shoppingCartRepository.delete(cart.id.Value);
    }
}