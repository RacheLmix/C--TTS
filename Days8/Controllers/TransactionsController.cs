using Microsoft.AspNetCore.Mvc;
using Days8.DTOs;
using Days8.Models;
using Days8.Data;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMapper _mapper;
    public TransactionsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Thêm giao dịch mới
    /// </summary>
    [HttpPost]
    public IActionResult CreateTransaction([FromBody] TransactionCreateDto dto)
    {
        // Validation
        if (dto.Amount <= 0)
            return BadRequest("Amount phải lớn hơn 0");
        if (dto.Date > DateTime.Now)
            return BadRequest("Date không được vượt quá ngày hiện tại");
        var department = InMemoryStorage.Departments.FirstOrDefault(d => d.Id == dto.DepartmentId);
        if (department == null)
            return BadRequest("Department không tồn tại");
        // Tính tổng chi hiện tại
        var totalExpense = InMemoryStorage.Transactions
            .Where(t => t.DepartmentId == dto.DepartmentId && t.Type == "Expense")
            .Sum(t => t.Amount);
        if (dto.Type == "Expense" && (totalExpense + dto.Amount) > department.BudgetLimit)
            return BadRequest("Tổng chi vượt quá BudgetLimit của phòng ban");
        // Map và lưu
        var transaction = _mapper.Map<Transaction>(dto);
        transaction.Id = InMemoryStorage.Transactions.Count > 0 ? InMemoryStorage.Transactions.Max(t => t.Id) + 1 : 1;
        transaction.Department = department;
        InMemoryStorage.Transactions.Add(transaction);
        department.Transactions.Add(transaction);
        return Ok(_mapper.Map<TransactionReadDto>(transaction));
    }

    /// <summary>
    /// Lọc giao dịch theo ngày, phòng ban, category
    /// </summary>
    [HttpGet]
    public IActionResult GetTransactions([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] int? departmentId, [FromQuery] string category)
    {
        var query = InMemoryStorage.Transactions.AsQueryable();
        if (fromDate.HasValue)
            query = query.Where(t => t.Date >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(t => t.Date <= toDate.Value);
        if (departmentId.HasValue)
            query = query.Where(t => t.DepartmentId == departmentId.Value);
        if (!string.IsNullOrEmpty(category))
            query = query.Where(t => t.Category == category);
        var result = query.Select(t => _mapper.Map<TransactionReadDto>(t)).ToList();
        return Ok(result);
    }
} 