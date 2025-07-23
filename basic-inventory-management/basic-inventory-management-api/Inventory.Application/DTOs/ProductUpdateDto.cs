using basic_inventory_management_api.Inventory.Domain.Enums;

namespace basic_inventory_management_api.Inventory.Application.DTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public ProductCategory Category { get; set; }
    }
}
