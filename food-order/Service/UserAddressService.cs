using Repository;
using Models;

namespace Service;
public class UserAddressService
{
    private readonly UserAddressRepository userAddressRepository;

    public UserAddressService(UserAddressRepository userAddressRepository)
    {
        this.userAddressRepository = userAddressRepository;
    }

    public async Task<ICollection<UserAddress>> findAll()
    {
        return await userAddressRepository.findAll();
    }

    public async Task<ICollection<UserAddress>> findByUsername(string username)
    {
        return await userAddressRepository.findByUsername(username);
    }

    public async Task<UserAddress> findById(int id)
    {
        return await userAddressRepository.findById(id);
    }

    public async Task<UserAddress> save(UserAddress address)
    {
        return await userAddressRepository.save(address);
    }

    public async Task<UserAddress> edit(UserAddress address, int id)
    {
        address.id = id;
        return await userAddressRepository.save(address);
    }

    public async Task deleteById(int id)
    {
        await userAddressRepository.delete(id);
    }
}