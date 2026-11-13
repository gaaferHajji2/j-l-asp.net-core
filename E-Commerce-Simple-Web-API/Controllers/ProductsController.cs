using E_Commerce_Simple_Web_API.DTOs;
using E_Commerce_Simple_Web_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Simple_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/products
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductListDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching products");
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with ID: {ProductId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching the product");
            }
        }

        // POST: api/products
        [HttpPost]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDetailDto>> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var createdProduct = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error while creating product");
                return BadRequest(new { errors = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while creating product");
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the product");
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailDto>> Update(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, dto);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product not found for update");
                return NotFound(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating product");
                return BadRequest(new { errors = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while updating product");
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID: {ProductId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product");
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Product with ID {id} not found or already deleted");
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product not found for deletion");
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID: {ProductId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product");
            }
        }
    }
}
