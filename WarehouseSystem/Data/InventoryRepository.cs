using System;
using System.Collections.Generic;
using WarehouseInventory.Exceptions;
using WarehouseInventory.Interfaces;

namespace WarehouseInventory.Data
{
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new();

        public void AddItem(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException($"Item with Id {item.Id} already exists.");
            _items[item.Id] = item;
        }

        public T GetItemById(int id)
        {
            if (_items.TryGetValue(id, out var item)) return item;
            throw new ItemNotFoundException($"Item with Id {id} was not found.");
        }

        public void RemoveItem(int id)
        {
            if (!_items.Remove(id))
                throw new ItemNotFoundException($"Cannot remove: Id {id} not found.");
        }

        public List<T> GetAllItems() => new(_items.Values);

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException($"Quantity cannot be negative (attempted {newQuantity}).");

            var item = GetItemById(id); 
            item.Quantity = newQuantity;
        }
    }
}
