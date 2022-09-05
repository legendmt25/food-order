using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>
{
    public ShoppingCartRepository(ApplicationContext context) : base(context) { }

    public async Task<ICollection<ShoppingCart>> findAll()
    {
        return await entries.Include("user").Include("footCartEntry").Include("foodCartEntry.items").ToListAsync();
    }

    public async Task<ShoppingCart> findById(int id)
    {
        return await entries.Include("user").Include("footCartEntry").Include("foodCartEntry.items").FirstOrDefaultAsync(entry => entry.id.Equals(id));
    }

    public async Task<ShoppingCart> findByUsername(string username)
    {
        return await entries.Include("user").Include("footCartEntry").Include("foodCartEntry.items").FirstOrDefaultAsync(entry => entry.user.UserName.Equals(username));
    }

    public async Task<ShoppingCart> save(ShoppingCart cart)
    {
        EntityEntry<ShoppingCart> updated = context.Update(cart);
        await context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task delete(int id)
    {
        ShoppingCart entry = await entries.FindAsync(id);
        context.Remove(entry);
        await context.SaveChangesAsync();
    }

}