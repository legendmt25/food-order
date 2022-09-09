using System.Text.Json.Serialization;

namespace Models;

public class ShoppingCart
{
    public int? id { get; set; }

    [JsonIgnore]
    public AppUser user { get; set; }
    public FoodCartEntry foodCartEntry { get; set; }


    public ShoppingCart() { }
    public ShoppingCart(FoodCartEntry foodCartEntry, AppUser user)
    {
        this.foodCartEntry = foodCartEntry;
        this.user = user;
    }
}