using E_Commerce_Simple_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Simple_Web_API.Configurations
{
    public static class ProductConfiguration
    {
        public static void ConfigureProduct(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Table name
                entity.ToTable("products");

                // Primary key
                entity.HasKey(p => p.Id);

                // Properties configuration
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(p => p.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("price");

                entity.Property(p => p.StockQuantity)
                    .IsRequired()
                    .HasColumnName("stock_quantity");

                entity.Property(p => p.Sku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sku");

                entity.Property(p => p.IsActive)
                    .IsRequired()
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");

                entity.Property(p => p.IsDeleted)
                    .IsRequired()
                    .HasDefaultValue(false)
                    .HasColumnName("is_deleted");

                entity.Property(p => p.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("created_at");

                entity.Property(p => p.UpdatedAt)
                    .HasColumnName("updated_at");

                entity.Property(p => p.DeletedAt)
                    .HasColumnName("deleted_at");

                entity.Property(p => p.CategoryId)
                    .HasColumnName("category_id");

                // Indices for better query performance
                entity.HasIndex(p => p.Sku)
                    .IsUnique()
                    .HasDatabaseName("idx_products_sku_unique");

                entity.HasIndex(p => p.Name)
                    .HasDatabaseName("idx_products_name");

                entity.HasIndex(p => p.IsActive)
                    .HasDatabaseName("idx_products_is_active");

                entity.HasIndex(p => p.IsDeleted)
                    .HasDatabaseName("idx_products_is_deleted");

                entity.HasIndex(p => new { p.IsDeleted, p.IsActive })
                    .HasDatabaseName("idx_products_deleted_active");

                // Relationship with Category (optional)
                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
