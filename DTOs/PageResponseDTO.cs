namespace prof_edna_teles_shop_api.DTOs;

public class PageResponseDTO
{
    public string Name { get; set; }
    public short Version { get; set; }

    public byte ProductMiniRange { get; set; }
    public List<ProductMiniResponseDTO> ProductsMini { get; set; } = [];
    
    public byte ProductRange { get; set; }
    public List<ProductResponseDTO> Products { get; set; } = [];

    public byte CategoryRange { get; set; }
    public List<CategoryResponseDTO> Categories { get; set; } = [];

    public byte UserRange { get; set; }
    public List<UserResponseDTO> Users { get; set; } = [];
}
