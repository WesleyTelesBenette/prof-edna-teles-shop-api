using Microsoft.Data.SqlClient;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRep;
    private readonly IProductService _productService;

    public UserService(IUserRepository userRep, IProductService productService)
    {
        _userRep = userRep;
        _productService = productService;
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        try
        {
            return await _userRep.GetAllUsersAsync();
        }
        catch (SqlException ex)
        {
            throw new Exception("Falha ao acessar o banco de dados. Verifique a conectividade.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new TimeoutException("O tempo de execução excedeu o limite permitido.", ex);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro ao tentar buscar os usuários.", e);
        }
    }

    public async Task<User?> GetUserByIdAsync(long id)
    {
        try
        {
            return await _userRep.GetUserByIdAsync(id);
        }
        catch (SqlException ex)
        {
            throw new Exception("Falha ao acessar o banco de dados. Verifique a conectividade.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new TimeoutException("O tempo de execução excedeu o limite permitido.", ex);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro ao tentar buscar o usuário.", e);
        }
    }

    public async Task<User?> CreateUserAsync(UserPostDTO user)
    {
        try
        {
            User newUser = new(user)
            {
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            var createdUser = await _userRep.CreateUserAsync(newUser);

            return createdUser ?? throw new InvalidOperationException("Falha ao criar usuário.");
        }
        catch (SqlException ex)
        {
            throw new Exception("Falha ao acessar o banco de dados. Verifique a conectividade.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new TimeoutException("O tempo de execução excedeu o limite permitido.", ex);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro ao tentar criar o usuário.", e);
        }
    }

    public async Task<bool> UpdateUserAsync(UserPutDTO user)
    {
        try
        {
            User? userFound = await _userRep.GetUserByIdAsync(user.Id);

            if (userFound != null)
            {
                userFound.Name = user.Name ?? userFound.Name;
                userFound.Email = user.Email ?? userFound.Email;

                userFound.Password = (user.Password != null)
                    ? BCrypt.Net.BCrypt.HashPassword(user.Password)
                    : userFound.Password;

                userFound.Products = (user.ProductsId != null)
                    ? await _productService.GetProductsByIdsAsync(user.ProductsId)
                    : userFound.Products;

                return await _userRep.UpdateUserAsync(userFound);
            }

            throw new KeyNotFoundException("Usuário com o ID fornecido não foi encontrado.");
        }
        catch (SqlException ex)
        {
            throw new Exception("Falha ao acessar o banco de dados. Verifique a conectividade.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new TimeoutException("O tempo de execução excedeu o limite permitido.", ex);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro ao tentar atualizar o usuário.", e);
        }
    }

    public async Task<bool> DeleteUserByIdAsync(long id)
    {
        try
        {
            User? user = await _userRep.GetUserByIdAsync(id);

            if (user != null)
            {
                if (await _userRep.DeleteUserAsync(user))
                {
                    return true;
                }

                throw new InvalidOperationException("Falha ao excluir usuário.");
            }

            throw new KeyNotFoundException("Usuário com o ID fornecido não foi encontrado.");
        }
        catch (SqlException ex)
        {
            throw new Exception("Falha ao acessar o banco de dados. Verifique a conectividade.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new TimeoutException("O tempo de execução excedeu o limite permitido.", ex);
        }
        catch (Exception e)
        {
            throw new Exception("Ocorreu um erro ao tentar excluir o usuário.", e);
        }
    }
}

