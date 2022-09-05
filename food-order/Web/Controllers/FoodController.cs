using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

namespace Web.Controllers;

[ApiController]
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
    public async Task<Food> save(Food food)
    {
        return await foodService.save(food);
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