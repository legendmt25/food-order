using Models;

namespace Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>
{
    public ShoppingCartRepository(ApplicationContext context) : base(context) { }
}