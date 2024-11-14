using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class CategoryPostDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; }
}
