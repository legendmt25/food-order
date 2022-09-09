using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers;

[ApiController]
[EnableCors]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly FoodService foodService;
    public FoodController(FoodService foodService)
    {
        this.foodService = foodService;
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "getFoodEntries")]
    public async Task<IEnumerable<Food>> foods()
    {
        return await foodService.findAll();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "getFoodEntry")]
    public async Task<Food> food(int id)
    {
        return await foodService.findById(id);
    }

    [HttpPost]
    [SwaggerOperation(OperationId = "saveFoodEntry")]
    public async Task<Food> save([FromForm] FoodDto food)
    {
        Console.WriteLine(food.accessories.Count);
        MemoryStream stream = new MemoryStream();
        await food.image.CopyToAsync(stream);
        ImageData image = new ImageData(stream.ToArray(), food.image.ContentType);
        Food saved = new Food(food.name, food.description, food.category, food.accessories, food.price, image);
        return await foodService.save(saved);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(OperationId = "editFoodEntry")]
    public async Task<Food> edit(Food food, int id)
    {
        return await foodService.edit(food, id);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "deleteFoodEntry")]
    public async Task delete(int id)
    {
        await foodService.deleteById(id);
    }
}