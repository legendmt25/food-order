namespace Models;

public class Food
{
    public int? id { get; set; }
    public FoodCategory category { get; set; }

    public virtual ICollection<FoodAccessory> accessories { get; set; }

    public Food() { }

    public Food(FoodCategory category, ICollection<FoodAccessory> accessories)
    {
        this.category = category;
        this.accessories = accessories;
    }
}