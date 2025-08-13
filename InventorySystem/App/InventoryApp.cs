using System;
using InventoryRecords.Data;
using InventoryRecords.Models;

namespace InventoryRecords.App
{
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "USB-C Cable", 120, DateTime.UtcNow));
            _logger.Add(new InventoryItem(2, "Wireless Mouse", 45, DateTime.UtcNow));
            _logger.Add(new InventoryItem(3, "Keyboard", 30, DateTime.UtcNow));
            _logger.Add(new InventoryItem(4, "External SSD 1TB", 15, DateTime.UtcNow));
            _logger.Add(new InventoryItem(5, "Webcam 1080p", 25, DateTime.UtcNow));
        }

        public void SaveData() => _logger.SaveToFile();

        public void LoadData() => _logger.LoadFromFile();

        public void PrintAllItems()
        {
            Console.WriteLine("=== Inventory Items ===");
            foreach (var item in _logger.GetAll())
            {
                Console.WriteLine($"Id={item.Id}, Name={item.Name}, Qty={item.Quantity}, Added={item.DateAdded:u}");
            }
            Console.WriteLine();
        }
    }
}
