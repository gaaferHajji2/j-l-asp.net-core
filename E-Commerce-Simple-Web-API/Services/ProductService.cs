using AutoMapper;
using E_Commerce_Simple_Web_API.DTOs;
using E_Commerce_Simple_Web_API.Models;
using E_Commerce_Simple_Web_API.Repositories;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Simple_Web_API.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository,
            IValidationService validationService,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _validationService = validationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductListDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all products");

            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductListDto>>(products);
        }

        public async Task<ProductDetailDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting product by ID: {ProductId}", id);

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product not found with ID: {ProductId}", id);
                return null;
            }

            return _mapper.Map<ProductDetailDto>(product);
        }

        public async Task<ProductDetailDto> CreateAsync(CreateProductDto dto)
        {
            _logger.LogInformation("Creating new product");

            // Validate request
            var validationResult = _validationService.ValidateCreateProduct(dto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for create product: {Errors}",
                    string.Join(", ", validationResult.Errors));
                throw new ValidationException(string.Join("; ", validationResult.Errors));
            }

            // Check if SKU already exists
            if (await _productRepository.ExistsBySkuAsync(dto.Sku))
            {
                _logger.LogWarning("SKU already exists: {Sku}", dto.Sku);
                throw new InvalidOperationException($"Product with SKU '{dto.Sku}' already exists");
            }

            // Map DTO to entity
            var product = _mapper.Map<Product>(dto);

            // Save to database
            var createdProduct = await _productRepository.CreateAsync(product);

            _logger.LogInformation("Product created successfully with ID: {ProductId}", createdProduct.Id);
            return _mapper.Map<ProductDetailDto>(createdProduct);
        }

        public async Task<ProductDetailDto> UpdateAsync(int id, UpdateProductDto dto)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);

            // Validate request
            var validationResult = _validationService.ValidateUpdateProduct(dto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for update product: {Errors}",
                    string.Join(", ", validationResult.Errors));
                throw new ValidationException(string.Join("; ", validationResult.Errors));
            }

            // Check if product exists
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product not found for update with ID: {ProductId}", id);
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            // Check SKU uniqueness if SKU is being updated
            if (!string.IsNullOrEmpty(dto.Sku) && dto.Sku != existingProduct.Sku)
            {
                if (await _productRepository.ExistsBySkuAsync(dto.Sku))
                {
                    _logger.LogWarning("SKU already exists: {Sku}", dto.Sku);
                    throw new InvalidOperationException($"Product with SKU '{dto.Sku}' already exists");
                }
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(dto.Name))
                existingProduct.Name = dto.Name;

            if (dto.Description != null)
                existingProduct.Description = dto.Description;

            if (dto.Price.HasValue)
                existingProduct.Price = dto.Price.Value;

            if (dto.StockQuantity.HasValue)
                existingProduct.StockQuantity = dto.StockQuantity.Value;

            if (!string.IsNullOrEmpty(dto.Sku))
                existingProduct.Sku = dto.Sku;

            if (dto.IsActive.HasValue)
                existingProduct.IsActive = dto.IsActive.Value;

            if (dto.CategoryId.HasValue)
                existingProduct.CategoryId = dto.CategoryId;

            // Save changes
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);

            _logger.LogInformation("Product updated successfully with ID: {ProductId}", updatedProduct.Id);
            return _mapper.Map<ProductDetailDto>(updatedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);

            var result = await _productRepository.DeleteAsync(id);

            if (!result)
            {
                _logger.LogWarning("Product not found or already deleted with ID: {ProductId}", id);
                throw new KeyNotFoundException($"Product with ID {id} not found or already deleted");
            }

            _logger.LogInformation("Product deleted successfully with ID: {ProductId}", id);
            return result;
        }
    }
}
