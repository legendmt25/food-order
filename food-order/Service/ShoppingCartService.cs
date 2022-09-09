using Repository;
using Models;

namespace Service;

public class ShoppingCartService
{
    private readonly ShoppingCartRepository shoppingCartRepository;
    private readonly FoodService foodService;
    private readonly UserService userService;
    public ShoppingCartService(ShoppingCartRepository shoppingCartRepository, UserService userService, FoodService foodService)
    {
        this.shoppingCartRepository = shoppingCartRepository;
        this.userService = userService;
        this.foodService = foodService;
    }

    public async Task<ICollection<ShoppingCart>> findAll()
    {
        return await shoppingCartRepository.findAll();
    }

    public async Task<ShoppingCart> findById(int id)
    {
        return await shoppingCartRepository.findById(id);
    }

    public async Task<ShoppingCart> findByUsername(string username)
    {
        return await shoppingCartRepository.findByUsername(username);
    }

    public async Task addItem(string username, FoodAddItemDto foodAddItemDto)
    {
        ShoppingCart cart = await findByUsername(username);
        if (cart == null)
        {
            AppUser user = await userService.findByUsername(username);
            cart = new ShoppingCart(new FoodCartEntry(), user);
        }
        Food food = await this.foodService.findById(foodAddItemDto.foodId);
        cart.foodCartEntry.items.Add(new FoodCartItem(food, foodAddItemDto.quantity, foodAddItemDto.size));
        await shoppingCartRepository.save(cart);
    }

    public async Task removeItem(string username, int itemId)
    {
        ShoppingCart cart = await findByUsername(username);
        if (cart == null)
        {
            throw new HttpRequestException("You don't have a shopping cart");
        }
        FoodCartItem item = cart.foodCartEntry.items.FirstOrDefault(item => item.id.Equals(itemId));
        cart.foodCartEntry.items.Remove(item);
        await shoppingCartRepository.save(cart);
    }
}