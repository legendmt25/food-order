using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository;

public class OrderRepository : Repository<Order>
{
    public OrderRepository(ApplicationContext context) : base(context) { }
    public async Task<ICollection<Order>> findAll()
    {
        return await entries.Include("user").Include("foodOrderEntry").Include("foodOrderEntry.items").ToListAsync();
    }

    public async Task<ICollection<Order>> findByUsername(string username)
    {
        return await entries.Include("user")
            .Include("foodOrderEntry")
            .Include("foodOrderEntry.items")
            .Where((order) => order.user.UserName.Equals(username))
            .ToListAsync();
    }

    public async Task<Order> findById(int id)
    {
        return await entries.Include("user").Include("foodOrderEntry").Include("foodOrderEntry.items").FirstOrDefaultAsync(entry => entry.id.Equals(id));
    }

    public async Task<Order> save(Order cart)
    {
        EntityEntry<Order> updated = context.Update(cart);
        await context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task delete(int id)
    {
        Order entry = await entries.FindAsync(id);
        context.Remove(entry);
        await context.SaveChangesAsync();
    }
}