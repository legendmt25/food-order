namespace Models;

public class FoodAddItemDto
{
    public int foodId { get; set; }
    public int quantity { get; set; }

    public FoodAddItemDto() { }

    public FoodAddItemDto(int quantity, int foodId)
    {
        this.quantity = quantity;
        this.foodId = foodId;
    }


}