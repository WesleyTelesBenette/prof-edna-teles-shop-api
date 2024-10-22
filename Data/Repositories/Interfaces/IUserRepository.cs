using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(long id);
    public Task<User?> CreateUserAsync(User user);
    public Task<bool> UpdateUserAsync(User user);
    public Task<bool> DeleteUserAsync(User user);
}
