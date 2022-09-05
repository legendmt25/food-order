

namespace Models;

public class ShoppingCart
{
    public int id { get; set; }
    public virtual ICollection<Food> items { get; set; }


    public ShoppingCart() { }
    public ShoppingCart(ICollection<Food> items)
    {
        this.items = items;
    }
}