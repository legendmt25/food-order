namespace Models;

public class FoodAddItemDto
{
    public int foodId { get; set; }
    public int quantity { get; set; }
    public FoodSize size { get; set; }

    public FoodAddItemDto() { }

    public FoodAddItemDto(int quantity, int foodId, FoodSize size)
    {
        this.quantity = quantity;
        this.foodId = foodId;
        this.size = size;
    }


}