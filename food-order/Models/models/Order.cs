namespace Models;

public class Order
{
    public int? id { get; set; }
    public AppUser user { get; set; }
    public FoodCartEntry foodOrderEntry { get; set; }

    public Order() { }

    public Order(FoodCartEntry foodOrderEntry, AppUser user)
    {
        this.foodOrderEntry = foodOrderEntry;
        this.user = user;
    }
}