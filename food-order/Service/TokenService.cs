using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Service;

public class TokenService
{
    private readonly SymmetricSecurityKey KEY;
    private readonly String ISSUER;
    private readonly string AUDIENCE;

    public TokenService(IConfiguration config)
    {
        IConfigurationSection jwtConfig = config.GetSection("jwtConfig");
        this.KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.GetSection("secretKey").Value));
        this.ISSUER = jwtConfig.GetSection("validIssuer").Value;
        this.AUDIENCE = jwtConfig.GetSection("validAudience").Value;
    }

    public string createToken(AppUser user, ICollection<string> roles)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        
        IEnumerable<Claim> claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).Append(new Claim(ClaimTypes.Name, user.UserName));
        
        SigningCredentials credentials = new SigningCredentials(KEY, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Issuer = ISSUER;
        tokenDescriptor.Audience = AUDIENCE;
        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        tokenDescriptor.Expires = DateTime.Now.AddHours(2);
        tokenDescriptor.SigningCredentials = credentials;

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}