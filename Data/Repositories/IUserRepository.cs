using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories;

public interface IUserRepository
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(long id);
    public Task<User?> CreateUserAsync(User user);
    public Task<bool> UpdateUserAsync(long id, UserPostDTO user);
    public Task<bool> DeleteAsync(long id);
}
