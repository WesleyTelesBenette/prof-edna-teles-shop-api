using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface IUserService
{
    public Task<ICollection<UserResponseDTO>> GetAllUsersAsync();
    public Task<UserResponseDTO?> GetUserByIdAsync(long id);
    public Task<ICollection<ProductResponseDTO>> GetProductsPurchased(string email);
    public Task<UserResponseDTO?> CreateUserAsync(UserPostDTO user);
    public Task<bool> UpdateUserAsync(UserPutDTO user);
    public Task<bool> DeleteUserByIdAsync(long id);
}
