using oop_lab1.Results;

namespace oop_lab1;

public class Truck : SkuHolder
{
    private double Speed { get; }
    private Coordinates Location { get; set; }

    public Truck(double capacity, double speed, Coordinates location) : base(capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(speed);
        Speed = speed;
        Location = location;
    }

    public TimeSpan MoveTo(Coordinates target)
    {
        var distance = DistanceCalculator.Distance(Location, target);
        var hours = distance / Speed;
        Location = target;
        return TimeSpan.FromHours(hours);
    }

    public TruckLoadResult Load(Manifest manifest)
    {
        if (TryAddSku(manifest))
        {
            return new TruckLoadResult.LoadSuccess();
        }

        return new TruckLoadResult.LoadFailure(manifest);
    }

    public TruckUnloadResult Unload(Manifest manifest)
    {
        if (TryRemoveSku(manifest))
        {
            return new TruckUnloadResult.UnloadSuccess();
        }
        
        return new TruckUnloadResult.UnloadFailure(manifest);
    }
}
