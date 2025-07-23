using basic_inventory_management_api.Inventory.Domain.Enums;
using basic_inventory_management_api.Inventory.Domain.Models;

namespace basic_inventory_management_api.Inventory.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(Guid id);
        IEnumerable<Product> Search(string? name = null, ProductCategory? category = null);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteById (Guid id);  

    }
}
