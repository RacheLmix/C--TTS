using System.Collections.Generic;
using Days8.DTOs;

public interface IReportService
{
    CashFlowReportDto GetCashFlowReport(int month, int? departmentId);
    IEnumerable<BudgetVarianceDto> GetBudgetVarianceReport();
}

public class BudgetVarianceDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public decimal BudgetLimit { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Variance { get; set; }
} 