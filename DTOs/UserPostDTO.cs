using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class UserPostDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail deve ser um endereço válido.")]
    [StringLength(200, ErrorMessage = "O e-mail não pode ter mais de 200 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
    public string Password { get; set; }

    public ICollection<long>? ProductsId { get; set; } = [];
}
