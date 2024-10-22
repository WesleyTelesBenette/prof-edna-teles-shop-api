using prof_edna_teles_shop_api.Models;
using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class UserPutDTO
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string? Name { get; set; } = null;

    [EmailAddress(ErrorMessage = "O e-mail deve ser um endereço válido.")]
    [StringLength(200, ErrorMessage = "O e-mail não pode ter mais de 200 caracteres.")]
    public string? Email { get; set; } = null;

    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
    public string? Password { get; set; } = null;

    public HashSet<long>? ProductsId { get; set; } = null;
}
