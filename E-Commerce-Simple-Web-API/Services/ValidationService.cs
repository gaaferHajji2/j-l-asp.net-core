using E_Commerce_Simple_Web_API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Simple_Web_API.Services
{

    public interface IValidationService
    {
        ValidationResult2 ValidateCreateProduct(CreateProductDto dto);
        ValidationResult2 ValidateUpdateProduct(UpdateProductDto dto);
    }

    public class ValidationService : IValidationService
    {
        public ValidationResult2 ValidateCreateProduct(CreateProductDto dto)
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

            if (!isValid)
            {
                return new ValidationResult2(false, results.Select(r => r.ErrorMessage).ToList());
            }

            // Additional business logic validation
            if (dto.Price <= 0)
            {
                return new ValidationResult2(false, new List<string> { "Price must be greater than zero" });
            }

            return new ValidationResult2(true, new List<string>());
        }
        public ValidationResult2 ValidateUpdateProduct(UpdateProductDto dto)
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

            if (!isValid)
            {
                return new ValidationResult2(false, results.Select(r => r.ErrorMessage).ToList());
            }

            return new ValidationResult2(true, new List<string>());
        }
    }

    public record ValidationResult2(bool IsValid, List<string> Errors);
}
