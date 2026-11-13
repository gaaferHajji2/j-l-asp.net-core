using E_Commerce_Simple_Web_API.DTOs;

namespace E_Commerce_Simple_Web_API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetAllAsync();
        Task<ProductDetailDto?> GetByIdAsync(int id);
        Task<ProductDetailDto> CreateAsync(CreateProductDto dto);
        Task<ProductDetailDto> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
