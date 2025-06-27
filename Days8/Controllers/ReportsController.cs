using Microsoft.AspNetCore.Mvc;
using Days8.DTOs;
using Days8.Models;
using Days8.Data;
using AutoMapper;
using System.Globalization;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMapper _mapper;
    public ReportsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Thống kê tổng dòng tiền, lợi nhuận theo tháng và phòng ban
    /// </summary>
    [HttpGet("cashflow")]
    public IActionResult GetCashFlow([FromQuery] int month, [FromQuery] int? departmentId)
    {
        var query = InMemoryStorage.Transactions.AsQueryable();
        if (month > 0 && month <= 12)
            query = query.Where(t => t.Date.Month == month);
        if (departmentId.HasValue)
            query = query.Where(t => t.DepartmentId == departmentId.Value);
        var totalIncome = query.Where(t => t.Type == "Income").Sum(t => t.Amount);
        var totalExpense = query.Where(t => t.Type == "Expense").Sum(t => t.Amount);
        var netProfit = totalIncome - totalExpense;
        decimal budgetVariance = 0;
        if (departmentId.HasValue)
        {
            var dept = InMemoryStorage.Departments.FirstOrDefault(d => d.Id == departmentId.Value);
            if (dept != null)
                budgetVariance = dept.BudgetLimit - totalExpense;
        }
        var report = new CashFlowReportDto
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            NetProfit = netProfit,
            BudgetVariance = budgetVariance
        };
        return Ok(report);
    }

    /// <summary>
    /// So sánh chi tiêu với ngân sách từng phòng ban
    /// </summary>
    [HttpGet("budget-variance")]
    public IActionResult GetBudgetVariance()
    {
        var result = InMemoryStorage.Departments.Select(dept => new
        {
            DepartmentId = dept.Id,
            DepartmentName = dept.Name,
            BudgetLimit = dept.BudgetLimit,
            TotalExpense = dept.Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount),
            Variance = dept.BudgetLimit - dept.Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount)
        }).ToList();
        return Ok(result);
    }
} 