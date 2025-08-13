using System;
using System.IO;
using InventoryRecords.App;

namespace InventoryRecords
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string filePath = args.Length > 0 ? args[0] : Path.Combine("data", "inventory_log.json");

            var app = new InventoryApp(filePath);
            app.SeedSampleData();
            app.SaveData();

            app = null; // simulate new session

            var app2 = new InventoryApp(filePath);
            app2.LoadData();
            app2.PrintAllItems();

            Console.WriteLine($"Data persisted to: {filePath}");
        }
    }
}
