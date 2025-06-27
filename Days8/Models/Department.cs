namespace Days8.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BudgetLimit { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
} 