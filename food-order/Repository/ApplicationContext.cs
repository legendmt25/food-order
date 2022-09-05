using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Repository;

public class ApplicationContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Food> foods { get; set; }
    public DbSet<AppUser> users { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<ShoppingCart> shoppingCarts { get; set; }

}
