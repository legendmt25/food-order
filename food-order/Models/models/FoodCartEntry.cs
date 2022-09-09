namespace Models;

public class FoodCartEntry
{
    public int? id { get; set; }
    public virtual ICollection<FoodCartItem> items { get; set; }

    public FoodCartEntry() { items = new List<FoodCartItem>(); }

    public FoodCartEntry(ICollection<FoodCartItem> items)
    {
        this.items = items;
    }
}