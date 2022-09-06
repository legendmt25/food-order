namespace Models;

public class Food
{
    public int? id { get; set; }
    public FoodCategory category { get; set; }
    public decimal price { get; set; }

    public virtual ICollection<FoodAccessory> accessories { get; set; }

    public Food() { }

    public Food(FoodCategory category, ICollection<FoodAccessory> accessories, decimal price)
    {
        this.category = category;
        this.accessories = accessories;
        this.price = price;
    }
}