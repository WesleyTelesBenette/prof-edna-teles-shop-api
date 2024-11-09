      using Microsoft.EntityFrameworkCore;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(j => j.ToTable("prof_product_category"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.Products)
            .WithMany(p => p.Users)
            .UsingEntity(j => j.ToTable("prof_product_user"));

        base.OnModelCreating(modelBuilder);
    }
}
