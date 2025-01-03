﻿using System.ComponentModel.DataAnnotations;

namespace prof_edna_teles_shop_api.DTOs;

public class ProductPutDTO
{
    [Required(ErrorMessage = "O Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Id deve ser um número positivo.")]
    public long Id { get; set; }

    [StringLength(200, ErrorMessage = "O nome não pode ter mais de 200 caracteres.")]
    public string? Name { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
    public long? PriceInCents { get; set; } = 0;

    [MinLength(1, ErrorMessage = "É necessário fornecer ao menos uma categoria.")]
    public HashSet<long>? CategoriesIds { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
    public string? Description { get; set; }

    [MinLength(1, ErrorMessage = "É necessário fornecer pelo menos uma imagem.")]
    public List<string>? Images { get; set; }

    [RegularExpression("^(game|pack)$", ErrorMessage = "O tipo deve ser 'game' ou 'pack'.")]
    public string? Type { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPieces deve ser um número positivo.")]
    public long? TotalPieces { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalPlayers deve ser um número positivo.")]
    public long? TotalPlayers { get; set; }

    [Range(0, long.MaxValue, ErrorMessage = "O TotalGames deve ser um número positivo.")]
    public long? TotalGames { get; set; }

    [StringLength(50, ErrorMessage = "O tipo do jogo não pode ter mais de 50 caracteres.")]
    public string? GameType { get; set; }

    public HashSet<long>? IncludeGamesIds { get; set; }
}
