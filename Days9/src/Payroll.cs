namespace SalarySystem;

public class Employee {
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
}

public class SalaryCalculator {
    public decimal CalculateNetSalary(Employee emp) {
        if (emp == null) throw new ArgumentNullException(nameof(emp));
        if (emp.BaseSalary < 0 || emp.Deduction < 0) throw new ArgumentException("Invalid amount");
        
        return emp.BaseSalary + emp.Bonus - emp.Deduction;
    }
}

public interface IEmployeeRepository {
    Employee GetById(int id);
}

public class PayrollService {
    private readonly IEmployeeRepository _repo;
    public PayrollService(IEmployeeRepository repo) {
        _repo = repo;
    }

    public decimal GetNetSalary(int employeeId) {
        var emp = _repo.GetById(employeeId);
        if (emp == null)
        {
            throw new InvalidOperationException("Employee not found");
        }
        var calc = new SalaryCalculator();
        return calc.CalculateNetSalary(emp);
    }
}
