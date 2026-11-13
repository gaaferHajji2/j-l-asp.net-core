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
}
