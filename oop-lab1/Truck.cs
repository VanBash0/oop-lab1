namespace oop_lab1;

public class Truck
{
    public double Capacity { get; }
    public double Velocity { get; }
    public Coordinates Location { get; private set; }
    private Dictionary<SkuId, uint> _skus;

    public Truck(double capacity, double velocity, Coordinates location)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(velocity);
        Capacity = capacity;
        Velocity = velocity;
        Location = location;
        _skus = new Dictionary<SkuId, uint>();
    }
}
