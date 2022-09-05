namespace Models;

public class FoodCartEntry
{
    public int? id { get; set; }
    public virtual ICollection<FoodCartItem> items { get; set; }

    public FoodCartEntry() { }

    public FoodCartEntry(ICollection<FoodCartItem> items)
    {
        this.items = items;
    }
}