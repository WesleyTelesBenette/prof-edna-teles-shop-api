using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface IUserService
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(long id);
    public Task<User?> CreateUserAsync(UserPostDTO user);
    public Task<bool> UpdateUserAsync(UserPutDTO user);
    public Task<bool> DeleteUserByIdAsync(long id);
}
