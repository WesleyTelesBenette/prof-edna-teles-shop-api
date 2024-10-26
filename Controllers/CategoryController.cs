using Microsoft.AspNetCore.Mvc;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategorysAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        return (categories.Count > 0)
            ? Ok(categories)
            : NoContent();
    }

    [HttpGet("{id}", Name = "GetCategoryByIdAsync")]
    public async Task<IActionResult> GetCategoryByIdAsync(long id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        return (category != null)
            ? Ok(category)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryPostDTO newCategory)
    {
        if (ModelState.IsValid)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(newCategory);

            return (createdCategory != null)
                ? CreatedAtRoute("GetCategoryByIdAsync", new { id = createdCategory.Id }, createdCategory)
                : BadRequest("Falha ao criar o catergoria.");
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryPutDTO updatedCategory)
    {
        if (ModelState.IsValid)
        {
            var isUpdated = await _categoryService.UpdateCategoryAsync(updatedCategory);

            return (isUpdated)
                ? Ok()
                : BadRequest("Falha ao atualizar o catergoria.");
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryByIdAsync(long id)
    {
        var isDeleted = await _categoryService.DeleteCategoryByIdAsync(id);

        return (isDeleted)
            ? Ok()
            : BadRequest("Falha ao deletar catergoria.");
    }
}
