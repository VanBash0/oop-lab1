namespace oop_lab1;

public class Employee : IComparable<Employee>
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

    public int CompareTo(Employee? other)
    {
        if (other is null)
        {
            return 1;
        }
        
        var capacityComparison = other.Capacity.CompareTo(Capacity);
        if (capacityComparison == 0)
        {
            return LoadTime.CompareTo(other.LoadTime);
        }
        return capacityComparison;
    }
}
