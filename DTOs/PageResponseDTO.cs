namespace prof_edna_teles_shop_api.DTOs;

public class PageResponseDTO
{
    public string Name { get; set; }
    public short Version { get; set; }

    public string[][] Strings { get; set; } = [];

    public ProductMiniResponseDTO[][] ProductsMini { get; set; } = [];
    
    public ProductResponseDTO[][] Products { get; set; } = [];

    public CategoryResponseDTO[][] Categories { get; set; } = [];

    public UserResponseDTO[][] Users { get; set; } = [];
}
