using System.Text.Json.Serialization;

namespace Models;
public class UserAddress
{
    public int? id { get; set; }
    [JsonIgnore]
    public AppUser user { get; set; }
    public string city { get; set; }
    public string municipality { get; set; }
    public string address { get; set; }

    public UserAddress() { }
    public UserAddress(AppUser user, string city, string municipality, string address)
    {
        this.user = user;
        this.city = city;
        this.municipality = municipality;
        this.address = address;
    }
}
