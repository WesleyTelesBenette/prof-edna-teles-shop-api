using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prof_edna_teles_shop_api.Models;

[Table("prof_product")]
public class Product
{
    [Key]
    [Required(ErrorMessage = "O Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome não pode ter mais de 200 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0, long.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
    public long PriceInCents { get; set; }

    [Required(ErrorMessage = "A imagem de capa é obrigatória.")]
    [Url(ErrorMessage = "A imagem de capa deve ser uma URL válida.")]
    public string CoverImage { get; set; }

    [Required(ErrorMessage = "Pelo menos uma categoria é obrigatória.")]
    [MinLength(1, ErrorMessage = "É necessário fornecer ao menos uma categoria.")]
    public Category[] Categories { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Pelo menos uma imagem é obrigatória.")]
    [MinLength(1, ErrorMessage = "É necessário fornecer pelo menos uma imagem.")]
    public string[] Images { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [RegularExpression("^(game|pack)$", ErrorMessage = "O tipo deve ser 'game' ou 'pack'.")]
    public string Type { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPieces deve ser um número positivo.")]
    public long? TotalPieces { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPlayers deve ser um número positivo.")]
    public long? TotalPlayers { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalGames deve ser um número positivo.")]
    public long? TotalGames { get; set; }

    public ICollection<Product> IncludeGames { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public Product (string name,
        long priceInCents,
        string coverImage,
        Category[] categories,
        string description,
        string[] images,
        string type,
        long? totalPieces,
        long? totalPlayers,
        long? totalGames,
        Product[]? includeGames
    ) {
        Name = name;
        PriceInCents = priceInCents;
        CoverImage = coverImage;
        Categories = categories;
        Description = description;
        Images = images;
        Type = type;
        TotalPieces = totalPieces;
        TotalPlayers = totalPlayers;
        TotalGames = totalGames;
        IncludeGames = includeGames ?? [];
    }

    public Product() { }
}
