using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserAddressController : ControllerBase
{
    private readonly UserAddressService userAddressService;
    private readonly UserService userService;

    public UserAddressController(UserAddressService userAddressService, UserService userService)
    {
        this.userAddressService = userAddressService;
        this.userService = userService;
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "getAddressEntries")]
    public async Task<ICollection<UserAddress>> getAddressEntries()
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        return await this.userAddressService.findByUsername(User.Identity.Name);
    }

    [HttpPost]
    [SwaggerOperation(OperationId = "saveAddressEntry")]
    public async Task<UserAddress> saveAddressEntry(UserAddress address)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        AppUser user = await this.userService.findByUsername(User.Identity.Name);
        address.user = user;
        return await this.userAddressService.save(address);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(OperationId = "editAddressEntry")]
    public async Task<UserAddress> editAddressEntry(UserAddress address, int id)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        AppUser user = await this.userService.findByUsername(User.Identity.Name);
        address.user = user;
        return await this.userAddressService.edit(address, id);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "deleteAddressEntry")]
    public async Task deleteAddressEntry(int id)
    {
        if (User.Identity == null)
        {
            throw new HttpRequestException("Identity cannot be null");
        }
        await this.userAddressService.deleteById(id);
    }
}