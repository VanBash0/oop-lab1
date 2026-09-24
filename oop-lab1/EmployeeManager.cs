using System.Linq;

namespace oop_lab1;

public class EmployeeManager
{
    private readonly List<Employee> _employees;
    
    public EmployeeManager(IEnumerable<Employee> employees)
    {
        _employees = employees.ToList();
        _employees.Sort();
    }

    public TimeSpan CalculateLoadTime(Manifest manifest)
    {
        // TODO
        return TimeSpan.Zero;
    }

    public TimeSpan CalculateUnloadTime(Manifest manifest)
    {
        // TODO
        return TimeSpan.Zero;
    }
}
