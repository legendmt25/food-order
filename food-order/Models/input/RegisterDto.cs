
namespace Models;

public class RegisterDto
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public RegisterDto()
    {
    }

    public RegisterDto(string firstName, string lastName, string email, string username, string password)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.username = username;
        this.password = password;
    }
}