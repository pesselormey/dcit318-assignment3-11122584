using System;
using WarehouseInventory.App;
using WarehouseInventory.Models;
using WarehouseInventory.Exceptions;

namespace WarehouseInventory
{
    public static class Program
    {
        public static void Main()
        {
            var mgr = new WareHouseManager();

            mgr.SeedData();

            Console.WriteLine("=== GROCERIES ===");
            mgr.PrintAllItems(mgr.GroceriesRepo);
            Console.WriteLine();

            Console.WriteLine("=== ELECTRONICS ===");
            mgr.PrintAllItems(mgr.ElectronicsRepo);
            Console.WriteLine();

            Console.WriteLine("=== Test: Add Duplicate Electronic (Id=101) ===");
            try
            {
                mgr.ElectronicsRepo.AddItem(new ElectronicItem(101, "Ultrabook", 5, "Contoso", 24));
            }
            catch (DuplicateItemException die)
            {
                Console.WriteLine($"[AddItem] ERROR: {die.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("=== Test: Remove Non-existent Grocery (Id=999) ===");
            mgr.RemoveItemById(mgr.GroceriesRepo, 999);
            Console.WriteLine();

            Console.WriteLine("=== Test: Invalid Quantity Update (Id=102 -> -1000) ===");
            mgr.IncreaseStock(mgr.ElectronicsRepo, id: 102, quantity: -1000);
            Console.WriteLine();

            Console.WriteLine("=== Happy Path: Restock and Remove ===");
            mgr.IncreaseStock(mgr.GroceriesRepo, id: 202, quantity: 20);
            mgr.RemoveItemById(mgr.ElectronicsRepo, id: 103);

            Console.WriteLine("\n=== FINAL STATE: GROCERIES ===");
            mgr.PrintAllItems(mgr.GroceriesRepo);

            Console.WriteLine("\n=== FINAL STATE: ELECTRONICS ===");
            mgr.PrintAllItems(mgr.ElectronicsRepo);
        }
    }
}
