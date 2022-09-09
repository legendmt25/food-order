namespace Models;

public class FoodCartItem
{
    public int? id { get; set; }
    public Food food { get; set; }
    public int quantity { get; set; }
    public FoodSize size { get; set; }

    public FoodCartItem() { }

    public FoodCartItem(Food food, int quantity, FoodSize size)
    {
        this.food = food;
        this.quantity = quantity;
        this.size = size;
    }
}