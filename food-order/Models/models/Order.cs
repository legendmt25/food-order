using System.Text.Json.Serialization;

namespace Models;

public class Order
{
    public int? id { get; set; }
    [JsonIgnore]
    public AppUser user { get; set; }
    public FoodCartEntry foodOrderEntry { get; set; }
    public DateTime createdAt { get; set; }
    [JsonIgnore]
    public UserAddress address { get; set; }

    public Order()
    {
        createdAt = DateTime.Now;
    }

    public Order(FoodCartEntry foodOrderEntry, AppUser user, UserAddress address)
    {
        this.foodOrderEntry = foodOrderEntry;
        this.user = user;
        this.address = address;
        this.createdAt = DateTime.Now;
    }
}