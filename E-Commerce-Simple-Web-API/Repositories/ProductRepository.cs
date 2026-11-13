using Microsoft.EntityFrameworkCore;
using E_Commerce_Simple_Web_API.Models;

namespace E_Commerce_Simple_Web_API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id); // Lazy delete
        Task<bool> ExistsBySkuAsync(string sku);
        Task<bool> ExistsByIdAsync(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ECommerceDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all active products");
            return await _context.Products
                .Where(p => !p.IsDeleted && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product by ID: {ProductId}", id);
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _logger.LogInformation("Creating new product with SKU: {Sku}", product.Sku);

            product.CreatedAt = DateTime.UtcNow;
            product.IsDeleted = false;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product created successfully with ID: {ProductId}", product.Id);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", product.Id);

            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product updated successfully with ID: {ProductId}", product.Id);
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Performing lazy delete for product with ID: {ProductId}", id);

            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted)
            {
                _logger.LogWarning("Product not found or already deleted with ID: {ProductId}", id);
                return false;
            }

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Product soft deleted successfully with ID: {ProductId}", id);
            return true;
        }

        public async Task<bool> ExistsBySkuAsync(string sku)
        {
            return await _context.Products
                .AnyAsync(p => p.Sku == sku && !p.IsDeleted);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Products
                .AnyAsync(p => p.Id == id && !p.IsDeleted);
        }
    }
}
