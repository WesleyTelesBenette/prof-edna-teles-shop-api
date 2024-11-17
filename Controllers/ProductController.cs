using Microsoft.AspNetCore.Mvc;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await _productService.GetAllProductsAsync();

        return (products.Count > 0)
            ? Ok(products)
            : NoContent();
    }

    [HttpGet("filter/{term}/{length}/{page}")]
    public async Task<IActionResult> GetFilteredPaginatedProducts(string term, int length, int page)
    {
        var filtered = await _productService.GetFilteredPaginatedProducts(term, length, page);

        return (filtered != null)
            ? Ok(filtered)
            : NoContent();
    }

    [HttpGet("{id}", Name = "GetProductByIdAsync")]
    public async Task<IActionResult> GetProductByIdAsync(long id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        return (product != null)
            ? Ok(product)
            : NotFound();
    }

    [HttpGet("recent/{size}")]
    public async Task<IActionResult> GetRecentProducts(int size)
    {
        var products = await _productService.GetRecentProducts(size);

        return (products != null)
            ? Ok(products)
            : NoContent();
    }

    [HttpGet("random/{size}")]
    public async Task<IActionResult> GetRandomProducts(int size)
    {
        var products = await _productService.GetRandomProducts(size);

        return (products != null)
            ? Ok(products)
            : NoContent();
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetAllTypeProducts()
    {
        var productsTypes = await _productService.GetAllTypeProducts();

        return (productsTypes != null)
            ? Ok(productsTypes)
            : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductPostDTO newProduct)
    {
        if (ModelState.IsValid)
        {
            var createdProduct = await _productService.CreateProductAsync(newProduct);

            return (createdProduct != null)
                ? CreatedAtRoute("GetProductByIdAsync", new { id = createdProduct.Id }, createdProduct)
                : BadRequest("Falha ao criar o produto.");
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync([FromBody] ProductPutDTO updatedProduct)
    {
        if (ModelState.IsValid)
        {
            var isUpdated = await _productService.UpdateProductAsync(updatedProduct);

            return (isUpdated)
                ? Ok()
                : BadRequest("Falha ao atualizar o produto.");
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductByIdAsync(long id)
    {
        var isDeleted = await _productService.DeleteProductByIdAsync(id);

        return (isDeleted)
            ? Ok()
            : BadRequest("Falha ao deletar produto.");
    }
}
