using basic_inventory_management_api.Inventory.Application.Interfaces;
using basic_inventory_management_api.Inventory.Domain.Enums;
using basic_inventory_management_api.Inventory.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basic_inventory_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("get-all-products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts().OrderBy(p => p.DateAdded);
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                _logger.LogWarning("Product not found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> Search(string? name = null, ProductCategory? category = null)
        {
            try
            {
                var products = _productService.Search(name, category);
                
                if(!products.Any())
                {
                    _logger.LogInformation("Product not found with either name or category");
                    return NotFound("Product not found");
                }
                _logger.LogInformation( "Search Successfull");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred during search");
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("add-a-product")]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _productService.AddProduct(product);
                _logger.LogInformation("Product Created");
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError("Product Creation failed", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateProduct(Guid Id, Product product)
        {
            var existingProduct = _productService.GetProductById(Id);
            if (existingProduct == null)
            {
                _logger.LogInformation($"Product with ID {Id} not found");
                return NotFound();
            }

            _productService.UpdateProduct(product);
            _logger.LogInformation("Product Updated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid Id)
        {
            var existingProduct = _productService.GetProductById(Id);
            if (existingProduct == null)
            {
                _logger.LogInformation($"Product with ID {Id} not found");
                return NotFound();
            }

            _productService.DeleteById(Id);
            return NoContent();
        }
    }
}
