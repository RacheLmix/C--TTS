using SalarySystem;
using Xunit;

namespace SalarySystem.Tests;

public class SalaryCalculatorTests
{
    private readonly SalaryCalculator _calculator = new();

    [Fact]
    public void CalculateNetSalary_WithValidInputs_ReturnsCorrectSalary()
    {
        // Arrange
        var emp = new Employee { Name = "John Doe", BaseSalary = 5000, Bonus = 1000, Deduction = 500 };

        // Act
        var netSalary = _calculator.CalculateNetSalary(emp);

        // Assert
        Assert.Equal(5500, netSalary);
    }

    [Fact]
    public void CalculateNetSalary_WithZeroBonusAndDeduction_ReturnsBaseSalary()
    {
        // Arrange
        var emp = new Employee { Name = "Jane Doe", BaseSalary = 4000, Bonus = 0, Deduction = 0 };

        // Act
        var netSalary = _calculator.CalculateNetSalary(emp);

        // Assert
        Assert.Equal(4000, netSalary);
    }

    [Fact]
    public void CalculateNetSalary_WithNegativeBaseSalary_ThrowsArgumentException()
    {
        // Arrange
        var emp = new Employee { Name = "Invalid", BaseSalary = -100, Bonus = 0, Deduction = 0 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.CalculateNetSalary(emp));
    }

    [Fact]
    public void CalculateNetSalary_WithNegativeDeduction_ThrowsArgumentException()
    {
        // Arrange
        var emp = new Employee { Name = "Invalid", BaseSalary = 5000, Bonus = 0, Deduction = -100 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _calculator.CalculateNetSalary(emp));
    }
    
    [Fact]
    public void CalculateNetSalary_WithNullEmployee_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _calculator.CalculateNetSalary(null!));
    }
} 