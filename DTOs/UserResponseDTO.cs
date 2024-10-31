using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.DTOs;

public class UserResponseDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public ICollection<long> ProductsIds { get; set; }

    public UserResponseDTO() {}

    public UserResponseDTO(long id, string name, string email, ICollection<long> productsIds)
    {
        Id = id;
        Name = name;
        Email = email;
        ProductsIds = productsIds ?? [];
    }

    public UserResponseDTO(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        ProductsIds = user.Products?.Select(p => p.Id).ToList() ?? [];
    }
}
