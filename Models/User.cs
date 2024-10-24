using prof_edna_teles_shop_api.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prof_edna_teles_shop_api.Models;

[Table("prof_user")]
public class User
{
    [Key]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

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

    public ICollection<Product> Products { get; set; }

    public User() {}

    public User(UserPostDTO user)
    {
        Name = user.Name;
        Email = user.Email;
        Password = user.Password;
        Products = [];
    }
}
