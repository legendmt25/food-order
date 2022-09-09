using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository;

public class FoodRepository : Repository<Food>
{
    public FoodRepository(ApplicationContext context) : base(context) { }

    public async Task<ICollection<Food>> findAll()
    {
        return await entries.Include("image").Include("accessories").ToListAsync();
    }

    public async Task<Food> findById(int id)
    {
        return await entries.Include("image").Include("accessories").FirstOrDefaultAsync(entry => entry.id.Equals(id));
    }

    public async Task<Food> save(Food food)
    {
        EntityEntry<Food> updated = context.Update(food);
        await context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task delete(int id)
    {
        Food entry = await entries.FindAsync(id);
        context.Remove(entry);
        await context.SaveChangesAsync();
    }

}