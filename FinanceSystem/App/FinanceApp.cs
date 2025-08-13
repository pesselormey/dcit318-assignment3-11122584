using System;
using System.Collections.Generic;
using FinanceManagementSystem.Accounts;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Processors;

namespace FinanceManagementSystem.App
{
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new();

        public void Run()
        {
            Console.WriteLine("=== Finance Management System Demo ===");

            var account = new SavingsAccount("SA-001", 1000m);
            Console.WriteLine($"Opened SavingsAccount {account.AccountNumber} with balance {account.Balance:C}\n");

            var t1 = new Transaction(1, DateTime.Today, 150.75m, "Groceries");
            var t2 = new Transaction(2, DateTime.Today, 250.00m, "Utilities");
            var t3 = new Transaction(3, DateTime.Today, 700.00m, "Entertainment");

            var mobile = new MobileMoneyProcessor();
            var bank = new BankTransferProcessor();
            var crypto = new CryptoWalletProcessor();

            mobile.Process(t1);
            bank.Process(t2);
            crypto.Process(t3);
            Console.WriteLine();

            account.ApplyTransaction(t1);
            account.ApplyTransaction(t2);
            account.ApplyTransaction(t3);
            Console.WriteLine();

            _transactions.AddRange(new[] { t1, t2, t3 });

            Console.WriteLine("Transactions recorded:");
            foreach (var tx in _transactions)
                Console.WriteLine($" - {tx}");

            Console.WriteLine($"\nFinal balance for {account.AccountNumber}: {account.Balance:C}");
            Console.WriteLine("=== End Demo ===");
        }
    }
}
