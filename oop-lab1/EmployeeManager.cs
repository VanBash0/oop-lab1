namespace oop_lab1;

public class EmployeeManager
{
    private readonly List<Employee> _employees;
    
    public EmployeeManager(List<Employee> employees)
    {
        _employees = employees;
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
