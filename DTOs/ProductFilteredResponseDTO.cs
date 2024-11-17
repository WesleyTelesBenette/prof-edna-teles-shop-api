namespace prof_edna_teles_shop_api.DTOs;

public class ProductFilteredResponseDTO
{
    public int Length { get; set; }

    public ICollection<ProductMiniResponseDTO> Products { get; set; }
}
