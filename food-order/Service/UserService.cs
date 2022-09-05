using Repository;
using Models;

namespace Service;

public class UserService
{
    private readonly UserRepository userRepository;

    public UserService(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<AppUser>> findAll()
    {
        return await userRepository.findAll();
    }

    public async Task<AppUser> findById(int id)
    {
        return await userRepository.findById(id);
    }

    public async Task<AppUser> findByUsername(string username)
    {
        return await userRepository.findByUsername(username);
    }

    public async Task save(AppUser user)
    {
        await userRepository.save(user);
    }

    public async Task edit(AppUser user, int id)
    {
        user.Id = id;
        await userRepository.save(user);
    }

    public async Task deleteById(int id)
    {
        await userRepository.deleteById(id);
    }

}