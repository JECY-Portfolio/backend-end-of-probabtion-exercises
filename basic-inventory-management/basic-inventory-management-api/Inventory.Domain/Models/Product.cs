using basic_inventory_management_api.Inventory.Domain.Enums;

namespace basic_inventory_management_api.Inventory.Domain.Models
{
    public class Product
    {


        public Guid Id { get;  set; } = Guid.NewGuid();
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public ProductCategory Category { get; set; }

        public DateTime DateAdded { get; private set; }

        public Product(string name, string description, double price, int quantityInStock, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
            Category = category;
            DateAdded = DateTime.UtcNow;

        }

    }
}
