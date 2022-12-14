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

    public async Task<decimal> getTotalPriceByUsername(string username)
    {
        ShoppingCart cart = await this.findByUsername(username);
        return cart.foodCartEntry.items.Sum(item =>
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

    public async Task addItem(string username, FoodAddItemDto foodAddItemDto)
    {
        ShoppingCart cart = await findByUsername(username);
        if (cart == null)
        {
            AppUser user = await userService.findByUsername(username);
            cart = new ShoppingCart(new FoodCartEntry(), user);
        }
        Food food = await this.foodService.findById(foodAddItemDto.foodId);
        cart.foodCartEntry.items.Add(new FoodCartItem(food, foodAddItemDto.quantity, foodAddItemDto.size, foodAddItemDto.accessories));
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