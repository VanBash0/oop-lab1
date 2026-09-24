namespace oop_lab1;

public class Employee
{
    private double Capacity { get; }
    private TimeSpan LoadTime { get; }
    private TimeSpan MoveTime { get; }

    public Employee(double capacity, TimeSpan loadTime, TimeSpan moveTime, Sku sku)
    {
        Capacity = capacity;
        LoadTime = loadTime;
        MoveTime = moveTime;
    }
}
