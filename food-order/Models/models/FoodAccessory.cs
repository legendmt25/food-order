using Microsoft.AspNetCore.Mvc;

namespace Models;

[ModelBinder(BinderType = typeof(ModelBinder))]
public class FoodAccessory
{

    public int? id { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }

    public FoodAccessory() { }

    public FoodAccessory(string name, decimal price)
    {
        this.price = price;
        this.name = name;
    }
}