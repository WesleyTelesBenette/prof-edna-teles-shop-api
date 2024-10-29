using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.DTOs;

public class CategoryResponseDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public CategoryResponseDTO() {}

    public CategoryResponseDTO(Category category)
    {
        Id = category.Id;
        Name = category.Name;
        ImageUrl = category.ImageUrl;
    }
}
