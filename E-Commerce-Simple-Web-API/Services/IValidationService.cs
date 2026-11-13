using E_Commerce_Simple_Web_API.DTOs;

namespace E_Commerce_Simple_Web_API.Services
{
    public interface IValidationService
    {
        ValidationResult2 ValidateCreateProduct(CreateProductDto dto);
        ValidationResult2 ValidateUpdateProduct(UpdateProductDto dto);
    }

    public record ValidationResult2(bool IsValid, List<string> Errors);
}
