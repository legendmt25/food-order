namespace Models;

public class Food
{
    public int? id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public FoodCategory category { get; set; }
    public decimal price { get; set; }
    public ImageData image { get; set; }

    public virtual ICollection<FoodAccessory> accessories { get; set; }

    public Food() { accessories = new List<FoodAccessory>(); }

    public Food(string name, string description, FoodCategory category, ICollection<FoodAccessory> accessories, decimal price, ImageData image)
    {
        this.name = name;
        this.category = category;
        this.accessories = accessories;
        this.price = price;
        this.image = image;
    }
}