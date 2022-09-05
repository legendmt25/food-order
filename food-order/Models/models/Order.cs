namespace Models;
public class Order
{
    public int? id { get; set; }
    public virtual ICollection<OrderItem> items { get; set; }

    public Order()
    {
    }
}