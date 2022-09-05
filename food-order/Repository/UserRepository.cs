using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public class UserRepository : Repository<AppUser>
{
    public UserRepository(ApplicationContext context) : base(context) { }

    public async Task<AppUser> findByUsername(string username)
    {
        return await entries.SingleAsync((entry) => entry.UserName.Equals(username));
    }

    public async Task<ICollection<AppUser>> findAll()
    {
        return await entries.ToListAsync();
    }

    public async Task<AppUser> findById(int id)
    {
        return await entries.FindAsync(id);
    }

    public async Task save(AppUser user)
    {
        context.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task deleteById(int id)
    {
        AppUser entry = await entries.FindAsync(id);
        context.Remove(entry);
        await context.SaveChangesAsync();
    }

}