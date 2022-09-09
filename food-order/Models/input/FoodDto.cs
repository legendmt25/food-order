using Microsoft.AspNetCore.Http;

namespace Models;
public class FoodDto
{
    public string name { get; set; }
    public string description { get; set; }
    public FoodCategory category { get; set; }
    public decimal price { get; set; }
    public IFormFile image { get; set; }
    public ICollection<FoodAccessory> accessories { get; set; }

    public FoodDto() { }

    public FoodDto(string name, string description, FoodCategory category, decimal price, IFormFile image, ICollection<FoodAccessory> accessories)
    {
        this.name = name;
        this.description = description;
        this.category = category;
        this.price = price;
        this.image = image;
        this.accessories = accessories;
    }
}