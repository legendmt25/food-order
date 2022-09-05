using Repository;
using Models;

namespace Service;
public class FoodService
{
    private readonly FoodRepository foodRepository;
    public FoodService(FoodRepository foodRepository)
    {
        this.foodRepository = foodRepository;
    }

    public async Task<IEnumerable<Food>> findAll()
    {
        return await foodRepository.findAll();
    }

    public async Task<Food> findById(int id)
    {
        return await foodRepository.findById(id);
    }

    public async Task<Food> save(Food food)
    {
        return await foodRepository.save(food);
    }

    public async Task<Food> edit(Food food, int id)
    {
        food.id = id;
        return await foodRepository.save(food);
    }

    public async Task deleteById(int id)
    {
        await foodRepository.delete(id);
    }

}
