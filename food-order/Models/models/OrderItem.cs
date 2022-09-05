namespace Models;

public class OrderItem
{
    public Food food { get; set; }
    public int quantity { get; set; }

    public OrderItem()
    {
    }

    public OrderItem(Food food, int quantity)
    {
        this.food = food;
        this.quantity = quantity;
    }
}