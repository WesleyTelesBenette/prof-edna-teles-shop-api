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
        return await _db.Users.OrderBy(u => u.Id).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        var userCreated = await _db.Users.AddAsync(user);
        int createdResult = await _db.SaveChangesAsync();

        return (createdResult > 0)
            ? userCreated.Entity
            : null;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        User? userFound = await GetUserByIdAsync(user.Id);

        if (userFound != null)
        {
            _db.Entry(userFound).CurrentValues.SetValues(user);
            
            int updatedResult = await _db.SaveChangesAsync();
            return (updatedResult > 0);
        }

        return false;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        _db.Remove(user);
        int deletedResult = await _db.SaveChangesAsync();

        return (deletedResult > 0);
    }
}
