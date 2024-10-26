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

    [HttpGet("{id}", Name = "GetProductByIdAsync")]
    public async Task<IActionResult> GetProductByIdAsync(long id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        return (product != null)
            ? Ok(product)
            : NotFound();
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
