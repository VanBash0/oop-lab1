namespace oop_lab1.Workforce;

public class Employee
{
    public double Capacity { get; }
    public TimeSpan LoadTime { get; }
    public TimeSpan MoveTime { get; }

    public Employee(double capacity, TimeSpan loadTime, TimeSpan moveTime)
    {
        Capacity = capacity;
        LoadTime = loadTime;
        MoveTime = moveTime;
    }
}
