using Microsoft.Data.SqlClient;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRep;

    public CategoryService(ICategoryRepository categoryRep)
    {
        _categoryRep = categoryRep;
    }

    public async Task<ICollection<Category>> GetAllCategoriesAsync()
    {
        try
        {
            return await _categoryRep.GetAllCategoriesAsync();
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
            throw new Exception("Ocorreu um erro ao tentar buscar as categorias.", e);
        }
    }

    public async Task<Category?> GetCategoryByIdAsync(long id)
    {
        try
        {
            return await _categoryRep.GetCategoryByIdAsync(id);
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
            throw new Exception("Ocorreu um erro ao tentar buscar a categoria.", e);
        }
    }

    public async Task<ICollection<Category>> GetCategoriesByIdsAsync(HashSet<long> ids)
    {
        try
        {
            return await _categoryRep.GetCategorysByIdsAsync(ids);
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
            throw new Exception("Ocorreu um erro ao tentar buscar as categorias.", e);
        }
    }

    public async Task<Category?> CreateCategoryAsync(CategoryPostDTO category)
    {
        try
        {
            Category newCategory = new(category);
            var createdCategory = await _categoryRep.CreateCategoryAsync(newCategory);

            return createdCategory ?? throw new InvalidOperationException("Falha ao criar categoria.");
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
            throw new Exception("Ocorreu um erro ao tentar criar a categoria.", e);
        }
    }

    public async Task<bool> UpdateCategoryAsync(CategoryPutDTO category)
    {
        try
        {
            Category? categoryFound = await _categoryRep.GetCategoryByIdAsync(category.Id);

            if (categoryFound != null)
            {
                categoryFound.Name = category.Name ?? categoryFound.Name;
                categoryFound.ImageUrl = category.ImageUrl ?? categoryFound.ImageUrl;

                return await _categoryRep.UpdateCategoryAsync(categoryFound);
            }

            throw new KeyNotFoundException("Categoria com o ID fornecido não foi encontrado.");
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
            throw new Exception("Ocorreu um erro ao tentar atualizar a categoria.", e);
        }
    }

    public async Task<bool> DeleteCategoryByIdAsync(long id)
    {
        try
        {
            Category? category = await _categoryRep.GetCategoryByIdAsync(id);

            if (category != null)
            {
                if (await _categoryRep.DeleteCategoryAsync(category))
                {
                    return true;
                }

                throw new InvalidOperationException("Falha ao excluir categoria.");
            }

            throw new KeyNotFoundException("Categoria com o ID fornecido não foi encontrado.");
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
            throw new Exception("Ocorreu um erro ao tentar excluir a categoria.", e);
        }
    }
}
