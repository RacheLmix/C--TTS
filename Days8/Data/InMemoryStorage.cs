using System;
using System.Collections.Generic;
using System.Linq;
using Days8.Models;

namespace Days8.Data
{
    public static class InMemoryStorage
    {
        public static List<Department> Departments { get; set; } = new List<Department>
        {
            new Department { Id = 1, Name = "Kế toán", BudgetLimit = 100_000_000M, Transactions = new List<Transaction>() },
            new Department { Id = 2, Name = "Marketing", BudgetLimit = 200_000_000M, Transactions = new List<Transaction>() },
            new Department { Id = 3, Name = "Nhân sự", BudgetLimit = 80_000_000M, Transactions = new List<Transaction>() }
        };

        public static List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
} 