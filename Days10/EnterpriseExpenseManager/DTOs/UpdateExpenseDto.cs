namespace EnterpriseExpenseManager.DTOs
{
    public class UpdateExpenseDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
    }
} 