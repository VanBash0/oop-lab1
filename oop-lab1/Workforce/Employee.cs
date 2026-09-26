using oop_lab1.SKU;

namespace oop_lab1.Workforce;

public class Employee
{
    public VolumeWeightChars Capacity { get; }
    public TimeSpan LoadTime { get; }
    public TimeSpan MoveTime { get; }

    public Employee(VolumeWeightChars capacity, TimeSpan loadTime, TimeSpan moveTime)
    {
        Capacity = capacity;
        LoadTime = loadTime;
        MoveTime = moveTime;
    }
}
