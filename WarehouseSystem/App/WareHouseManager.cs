using System;
using WarehouseInventory.Data;
using WarehouseInventory.Interfaces;
using WarehouseInventory.Models;
using WarehouseInventory.Exceptions;

namespace WarehouseInventory.App
{
    public class WareHouseManager
    {
        private readonly InventoryRepository<ElectronicItem> _electronics = new();
        private readonly InventoryRepository<GroceryItem> _groceries = new();

        public InventoryRepository<ElectronicItem> ElectronicsRepo => _electronics;
        public InventoryRepository<GroceryItem> GroceriesRepo => _groceries;

        public void SeedData()
        {
            try
            {
                _electronics.AddItem(new ElectronicItem(101, "Laptop", 15, "Contoso", 24));
                _electronics.AddItem(new ElectronicItem(102, "Smartphone", 40, "Fabrikam", 12));
                _electronics.AddItem(new ElectronicItem(103, "Headphones", 60, "Northwind", 6));

                _groceries.AddItem(new GroceryItem(201, "Rice 5kg", 120, DateTime.Today.AddMonths(12)));
                _groceries.AddItem(new GroceryItem(202, "Milk 1L", 80, DateTime.Today.AddDays(10)));
                _groceries.AddItem(new GroceryItem(203, "Eggs (Dozen)", 50, DateTime.Today.AddDays(14)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SeedData] Unexpected error: {ex.Message}");
            }
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("(no items)");
                return;
            }
            foreach (var item in items)
                Console.WriteLine(item);
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                int newQty = item.Quantity + quantity;
                repo.UpdateQuantity(id, newQty);
                Console.WriteLine($"[IncreaseStock] Id={id} new quantity: {newQty}");
            }
            catch (ItemNotFoundException inf)
            {
                Console.WriteLine($"[IncreaseStock] ERROR: {inf.Message}");
            }
            catch (InvalidQuantityException iqe)
            {
                Console.WriteLine($"[IncreaseStock] ERROR: {iqe.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"[RemoveItem] Removed Id={id}");
            }
            catch (ItemNotFoundException inf)
            {
                Console.WriteLine($"[RemoveItem] ERROR: {inf.Message}");
            }
        }
    }
}
