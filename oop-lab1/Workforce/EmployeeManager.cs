using oop_lab1.SKU;

namespace oop_lab1.Workforce;

public class EmployeeManager
{
    private readonly EmployeeProcessCalculator _processCalculator;

    public EmployeeManager(IEnumerable<Employee> employees)
    {
        var sortedEmployees = employees.OrderByDescending(employee => employee.Capacity).ToList();
        _processCalculator = new EmployeeProcessCalculator(sortedEmployees);
    }

    public TimeSpan CalculateLoadTime(Manifest manifest)
    {
        return _processCalculator.CalculateLoadTime(manifest);
    }

    public TimeSpan CalculateUnloadTime(Manifest manifest)
    {
        return _processCalculator.CalculateUnloadTime(manifest);
    }
}