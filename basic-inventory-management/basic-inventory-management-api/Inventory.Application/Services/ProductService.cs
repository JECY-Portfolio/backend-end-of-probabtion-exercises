using basic_inventory_management_api.Inventory.Application.Interfaces;
using basic_inventory_management_api.Inventory.Domain.Enums;
using basic_inventory_management_api.Inventory.Domain.Models;

namespace basic_inventory_management_api.Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private static readonly Dictionary<Guid, Product> _products = new();
        public ProductService() 
        {
            var product1 = new Product("Radio", "Tv", 40000, 20, ProductCategory.Electronics);
             var product2 = new Product("Think-like-a-man","A-Good-Book", 40000, 20, ProductCategory.Books);
        }
        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Invalid product data");
            }
            if(string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentNullException("Product name is required");
            }
            if(product.Price <= 0)
            {
                throw new ArgumentNullException("Price must be greater than 0   ");
            }
            if (product.QuantityInStock <= 0)
            {
                throw new ArgumentNullException("Quantity must be greater than 0  ");
            }

            _products[product.Id] = product;
        }

        public bool DeleteById(Guid id)
        {
           return _products.Remove(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.Values;
        }

        public Product GetProductById(Guid id)
        {
            _products.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> Search(string? name = null, ProductCategory? category = null)
        {
            return _products.Values
                  .Where(p =>
                  (string.IsNullOrEmpty(name) || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                  (!category.HasValue || p.Category == category));
        }

        public void UpdateProduct(Guid id,Product product)
        {
            if(_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
        }
    }
}
