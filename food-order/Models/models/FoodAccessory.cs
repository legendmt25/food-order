namespace Models;
public class FoodAccessory
{

    public int? id { get; set; }
    public string name { get; set; }
    public decimal price { get; set; }

    public FoodAccessory()
    {
    }

    public FoodAccessory(int id, string name, decimal price)
    {
        this.id = id;
        this.price = price;
        this.name = name;
    }
}