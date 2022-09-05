using Models;
using Repository;
namespace Service;

public class OrderService
{
    private readonly ShoppingCartRepository shoppingCartRepository;
    private readonly OrderRepository orderRepository;
    private readonly ShoppingCartService shoppingCartService;
    private readonly UserService userService;

    public OrderService(ShoppingCartService shoppingCartService, UserService userService, OrderRepository orderRepository, ShoppingCartRepository shoppingCartRepository)
    {
        this.shoppingCartService = shoppingCartService;
        this.userService = userService;
        this.orderRepository = orderRepository;
        this.shoppingCartRepository = shoppingCartRepository;
    }

    public async Task makeOrder(string username)
    {
        ShoppingCart cart = await shoppingCartService.findByUsername(username);
        AppUser user = await userService.findByUsername(username);
        Order order = new Order(cart.foodCartEntry, user);
        await orderRepository.save(order);
        await shoppingCartRepository.delete(cart.id.Value);
    }
}