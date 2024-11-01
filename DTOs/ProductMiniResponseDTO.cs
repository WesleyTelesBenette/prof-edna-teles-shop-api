using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.DTOs;

public class ProductMiniResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; }

    public long PriceInCents { get; set; }

    public ICollection<long> CategoriesIds { get; set; }

    public string ImageCover { get; set; }

    public string Type { get; set; }

    public ProductMiniResponseDTO() {}

    public ProductMiniResponseDTO(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        PriceInCents = product.PriceInCents;
        CategoriesIds = product.Categories.Select(c => c.Id).ToList();
        ImageCover = product.Images[0];
        Type = product.Type;
    }
}
