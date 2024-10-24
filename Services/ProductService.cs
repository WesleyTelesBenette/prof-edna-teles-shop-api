﻿using Microsoft.Data.SqlClient;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRep;
    private readonly ICategoryService _categoryService;

    public ProductService(IProductRepository productRep, ICategoryService categoryService)
    {
        _productRep = productRep;
        _categoryService = categoryService;
    }

    public async Task<ICollection<Product>> GetAllProductAsync()
    {
        try
        {
            return await _productRep.GetAllProductsAsync();
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
            throw new Exception("Ocorreu um erro ao tentar buscar os produtos.", e);
        }
    }

    public async Task<Product?> GetProductByIdAsync(long id)
    {
        try
        {
            return await _productRep.GetProductByIdAsync(id);
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
            throw new Exception("Ocorreu um erro ao tentar buscar o produto.", e);
        }
    }

    public async Task<ICollection<Product>> GetProductsByIdsAsync(HashSet<long> ids)
    {
        try
        {
            return await _productRep.GetProductsByIdsAsync(ids);
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
            throw new Exception("Ocorreu um erro ao tentar buscar os produtos.", e);
        }
    }

    public async Task<Product?> CreateProductAsync(ProductPostDTO product)
    {
        try
        {
            Product newProduct = new(product)
            {
                Categories = await _categoryService.GetCategoriesByIdsAsync(product.CategoriesIds)
            };

            var createdProduct = await _productRep.CreateProductAsync(newProduct);

            return createdProduct ?? throw new InvalidOperationException("Falha ao criar produto.");
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
            throw new Exception("Ocorreu um erro ao tentar criar o produto.", e);
        }
    }

    public async Task<bool> UpdateProductAsync(ProductPutDTO product)
    {
        try
        {
            Product? productFound = await _productRep.GetProductByIdAsync(product.Id);

            if (productFound != null)
            {
                productFound.Name         = product.Name         ?? productFound.Name;
                productFound.PriceInCents = product.PriceInCents ?? productFound.PriceInCents;
                productFound.Description  = product.Description  ?? productFound.Description;
                productFound.Images       = product.Images       ?? productFound.Images;
                productFound.Type         = product.Type         ?? productFound.Type;
                productFound.TotalPieces  = product.TotalPieces  ?? productFound.TotalPieces;
                productFound.TotalPlayers = product.TotalPlayers ?? productFound.TotalPlayers;
                productFound.TotalGames   = product.TotalGames   ?? productFound.TotalGames;

                productFound.Categories = (product.CategoriesIds != null)
                    ? await _categoryService.GetCategoriesByIdsAsync(product.CategoriesIds)
                    : productFound.Categories;

                productFound.IncludeGames = (product.IncludeGamesIds != null)
                    ? await GetProductsByIdsAsync(product.IncludeGamesIds)
                    : productFound.IncludeGames;

                   return await _productRep.UpdateProductAsync(productFound);
            }

            throw new KeyNotFoundException("Produto com o ID fornecido não foi encontrado.");
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
            throw new Exception("Ocorreu um erro ao tentar atualizar o produto.", e);
        }
    }

    public async Task<bool> DeleteProductByIdAsync(long id)
    {
        try
        {
            Product? product = await _productRep.GetProductByIdAsync(id);

            if (product != null)
            {
                if (await _productRep.DeleteProductAsync(product))
                {
                    return true;
                }

                throw new InvalidOperationException("Falha ao excluir produto.");
            }

            throw new KeyNotFoundException("Produto com o ID fornecido não foi encontrado.");
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
            throw new Exception("Ocorreu um erro ao tentar excluir o produto.", e);
        }
    }
}
