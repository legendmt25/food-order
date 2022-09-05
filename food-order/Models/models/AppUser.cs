using Microsoft.AspNetCore.Identity;
namespace Models;

public class AppUser : IdentityUser<int>
{
    public string firstName { get; set; }
    public string lastName { get; set; }

    public AppUser() { }

    public AppUser(string firstName, string lastName, string email, string username)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.Email = email;
        this.UserName = username;
    }
}