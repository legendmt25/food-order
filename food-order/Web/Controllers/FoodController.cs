using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

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
    public async Task<IEnumerable<Food>> foods()
    {
        return await foodService.findAll();
    }

    [HttpGet("{id}")]
    public async Task<Food> food(int id)
    {
        return await foodService.findById(id);
    }

    [HttpPost]
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
    public async Task<Food> edit(Food food, int id)
    {
        return await foodService.edit(food, id);
    }

    [HttpDelete("{id}")]
    public async Task delete(int id)
    {
        await foodService.deleteById(id);
    }
}