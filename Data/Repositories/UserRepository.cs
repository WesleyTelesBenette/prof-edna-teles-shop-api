using Microsoft.EntityFrameworkCore;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await _db.Users.ToListAsync(); ;
    }

    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id); ;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        var userCreated = await _db.Users.AddAsync(user);
        int createdResult = await _db.SaveChangesAsync();

        return (createdResult > 0)
            ? userCreated.Entity
            : null;
    }

    public async Task<bool> UpdateUserAsync(long id, UserPostDTO user)
    {
        User? userFound = await GetUserByIdAsync(id);

        if (userFound != null)
        {
            _db.Users.Attach(userFound);

            userFound.Name = user.Name ?? userFound.Name;
            userFound.Email = user.Email ?? userFound.Email;
            userFound.Products = user.Products ?? userFound.Products;

            userFound.Password = (user.Password != null)
                ? BCrypt.Net.BCrypt.HashPassword(user.Password)
                : userFound.Password;
            
            int updatedResult = await _db.SaveChangesAsync();
            return (updatedResult > 0);
        }

        return false;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        User? userFound = await GetUserByIdAsync(id);

        if (userFound != null)
        {
            _db.Remove(userFound);
            int deletedResult = await _db.SaveChangesAsync();

            return (deletedResult > 0);
        }

        return false;
    }
}
