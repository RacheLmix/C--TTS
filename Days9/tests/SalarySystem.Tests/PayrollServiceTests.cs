using Moq;
using SalarySystem;
using Xunit;

namespace SalarySystem.Tests;

public class PayrollServiceTests
{
    private readonly Mock<IEmployeeRepository> _mockRepo;
    private readonly PayrollService _payrollService;

    public PayrollServiceTests()
    {
        _mockRepo = new Mock<IEmployeeRepository>();
        _payrollService = new PayrollService(_mockRepo.Object);
    }

    [Fact]
    public void GetNetSalary_WhenEmployeeExists_ReturnsCorrectNetSalary()
    {
        // Arrange
        var employeeId = 1;
        var employee = new Employee { Id = employeeId, Name = "Test Employee", BaseSalary = 6000, Bonus = 1200, Deduction = 200 };
        _mockRepo.Setup(r => r.GetById(employeeId)).Returns(employee);

        // Act
        var result = _payrollService.GetNetSalary(employeeId);

        // Assert
        Assert.Equal(7000, result);
        _mockRepo.Verify(r => r.GetById(employeeId), Times.Once); // Verify that GetById was called
    }

    [Fact]
    public void GetNetSalary_WhenEmployeeDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var employeeId = 99;
        _mockRepo.Setup(r => r.GetById(employeeId)).Returns((Employee)null!);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _payrollService.GetNetSalary(employeeId));
    }
} 