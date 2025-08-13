using System;

namespace FinanceManagementSystem.Models
{
    public record Transaction
    {
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public decimal Amount { get; init; }
        public string Category { get; init; }

        public Transaction(int id, DateTime date, decimal amount, string category)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Category is required.", nameof(category));

            Id = id;
            Date = date;
            Amount = amount;
            Category = category.Trim();
        }

        public override string ToString() => $"#{Id} {Category} {Amount:C} on {Date:d}";
    }
}
