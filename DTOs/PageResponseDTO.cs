namespace prof_edna_teles_shop_api.DTOs;

public class PageResponseDTO
{
    public string Name { get; set; }
    public short Version { get; set; }

    public ICollection<ICollection<string>> Strings { get; set; } = [];

    public ICollection<ICollection<ProductMiniResponseDTO>> ProductsMini { get; set; } = [];

    public ICollection<ICollection<ProductResponseDTO>> Products { get; set; } = [];

    public ICollection<ICollection<CategoryResponseDTO>> Categories { get; set; } = [];

    public ICollection<ICollection<UserResponseDTO>> Users { get; set; } = [];
}
