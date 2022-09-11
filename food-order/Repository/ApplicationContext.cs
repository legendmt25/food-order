using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Repository;

public class ApplicationContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public DbSet<Food> foods { get; set; }
    public DbSet<AppUser> users { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<ShoppingCart> shoppingCarts { get; set; }
    public DbSet<UserAddress> userAddresses { get; set; }

    public ApplicationContext(DbContextOptions options) : base(options) { }
}
