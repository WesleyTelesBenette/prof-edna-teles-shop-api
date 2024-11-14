using prof_edna_teles_shop_api.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prof_edna_teles_shop_api.Models;

[Table("prof_category")]
public class Category
{
    [Key]
    [Required(ErrorMessage = "O Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = [];

    public Category() { }

    public Category(CategoryPostDTO category)
    {
        Name = category.Name;
    }
}
