namespace Models;

public class FoodAddItemDto
{
    public int foodId { get; set; }
    public int quantity { get; set; }
    public FoodSize size { get; set; }
    public ICollection<FoodAccessory> accessories { get; set; }

    public FoodAddItemDto()
    {
        this.accessories = new List<FoodAccessory>();
    }

    public FoodAddItemDto(int quantity, int foodId, FoodSize size, ICollection<FoodAccessory> accessories)
    {
        this.quantity = quantity;
        this.foodId = foodId;
        this.size = size;
        this.accessories = accessories;
    }


}