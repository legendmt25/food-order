namespace Models;

public class FoodCartItem
{
    public int? id { get; set; }
    public Food food { get; set; }
    public int quantity { get; set; }
    public FoodSize size { get; set; }
    public ICollection<FoodAccessory> accessories { get; set; }

    public FoodCartItem() {
        this.accessories = new List<FoodAccessory>();
    }

    public FoodCartItem(Food food, int quantity, FoodSize size, ICollection<FoodAccessory> accessories)
    {
        this.food = food;
        this.quantity = quantity;
        this.size = size;
        this.accessories = accessories;
    }
}