using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using InventoryRecords.Interfaces;

namespace InventoryRecords.Data
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required.", nameof(filePath));
            _filePath = filePath;
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _log.Add(item);
        }

        public List<T> GetAll() => new(_log);

        public void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            try
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                JsonSerializer.Serialize(fs, _log, options);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[SaveToFile] Access denied: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"[SaveToFile] Directory not found: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[SaveToFile] I/O error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SaveToFile] Unexpected error: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"[LoadFromFile] File not found: '{_filePath}'. Nothing loaded.");
                return;
            }

            try
            {
                using FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var loaded = JsonSerializer.Deserialize<List<T>>(fs) ?? new List<T>();

                _log.Clear();
                _log.AddRange(loaded);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[LoadFromFile] Access denied: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[LoadFromFile] Invalid JSON format: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[LoadFromFile] I/O error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoadFromFile] Unexpected error: {ex.Message}");
            }
        }
    }
}
