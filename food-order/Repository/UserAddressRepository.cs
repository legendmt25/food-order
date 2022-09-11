using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository;

public class UserAddressRepository : Repository<UserAddress>
{
    public UserAddressRepository(ApplicationContext context) : base(context) { }

    public async Task<ICollection<UserAddress>> findAll()
    {
        return await entries.Include("user").ToListAsync();
    }

    public async Task<UserAddress> findById(int id)
    {
        return await entries.Include("user").FirstOrDefaultAsync(entry => entry.id.Equals(id));
    }

    public async Task<ICollection<UserAddress>> findByUsername(string username)
    {
        return await entries.Include("user").Where(entry => entry.user.UserName.Equals(username)).ToListAsync();
    }

    public async Task<UserAddress> save(UserAddress address)
    {
        EntityEntry<UserAddress> updated = context.Update(address);
        await context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task delete(int id)
    {
        UserAddress entry = await entries.FindAsync(id);
        context.Remove(entry);
        await context.SaveChangesAsync();
    }
}