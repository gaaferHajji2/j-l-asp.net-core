using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Simple_Web_API.DTOs
{
    // GET - List view (only required data)
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Sku { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    // GET by ID - Full details
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Sku { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
    }

    // POST - Create product
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "SKU can only contain letters, numbers, hyphens, and underscores")]
        public string Sku { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public int? CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int? StockQuantity { get; set; }

        [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9\-_]+$", ErrorMessage = "SKU can only contain letters, numbers, hyphens, and underscores")]
        public string? Sku { get; set; }

        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
