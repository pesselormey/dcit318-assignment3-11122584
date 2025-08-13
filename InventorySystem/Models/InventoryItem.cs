using System;
using InventoryRecords.Interfaces;

namespace InventoryRecords.Models
{
    // Immutable Inventory record implementing the marker interface
    public record InventoryItem(
        int Id,
        string Name,
        int Quantity,
        DateTime DateAdded
    ) : IInventoryEntity;
}
