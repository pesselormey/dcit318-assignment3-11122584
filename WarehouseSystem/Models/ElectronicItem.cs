using System;
using WarehouseInventory.Interfaces;

namespace WarehouseInventory.Models
{
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");
            if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentException("Brand is required.", nameof(brand));
            if (warrantyMonths < 0) throw new ArgumentOutOfRangeException(nameof(warrantyMonths), "Warranty cannot be negative.");

            Id = id;
            Name = name.Trim();
            Quantity = quantity;
            Brand = brand.Trim();
            WarrantyMonths = warrantyMonths;
        }

        public override string ToString() =>
            $"[Electronic] Id={Id}, Name={Name}, Qty={Quantity}, Brand={Brand}, Warranty={WarrantyMonths} mo";
    }
}
