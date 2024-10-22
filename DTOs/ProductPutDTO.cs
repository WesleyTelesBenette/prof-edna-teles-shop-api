using prof_edna_teles_shop_api.Models;
using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class ProductPutDTO
{
    [StringLength(200, ErrorMessage = "O nome não pode ter mais de 200 caracteres.")]
    public string Name { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
    public long PriceInCents { get; set; }

    [MinLength(1, ErrorMessage = "É necessário fornecer ao menos uma categoria.")]
    public long[] CategoriesIds { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string Description { get; set; }

    [MinLength(1, ErrorMessage = "É necessário fornecer pelo menos uma imagem.")]
    public string[] Images { get; set; }

    [RegularExpression("^(game|pack)$", ErrorMessage = "O tipo deve ser 'game' ou 'pack'.")]
    public string Type { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPieces deve ser um número positivo.")]
    public long? TotalPieces { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPlayers deve ser um número positivo.")]
    public long? TotalPlayers { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalGames deve ser um número positivo.")]
    public long? TotalGames { get; set; }

    public ICollection<long> IncludeGamesIds { get; set; }
}
