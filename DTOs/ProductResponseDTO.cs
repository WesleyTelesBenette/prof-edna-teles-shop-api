﻿using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.DTOs;

public class ProductResponseDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long PriceInCents { get; set; }

    public string Description { get; set; }

    public string[] Images { get; set; }

    public string Type { get; set; }

    public long? TotalPieces { get; set; }

    public long? TotalPlayers { get; set; }

    public long? TotalGames { get; set; }

    public ICollection<long>? IncludeGames { get; set; }

    public ICollection<long> Categories { get; set; }

    public ProductResponseDTO() {}
    
    public ProductResponseDTO(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        PriceInCents = product.PriceInCents;
        Description = product.Description;
        Images = product.Images;
        Type = product.Type;
        TotalPieces = product.TotalPieces;
        TotalPlayers = product.TotalPlayers;
        TotalGames = product.TotalGames;
        IncludeGames = product.IncludeGames.Select(g => g.Id).ToList();
        Categories = product.Categories.Select(c => c.Id).ToList();
    }
}