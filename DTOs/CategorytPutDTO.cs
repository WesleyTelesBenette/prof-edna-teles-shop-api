using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class CategoryPutDTO
{
    [Key]
    [Required(ErrorMessage = "O Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; }
}
