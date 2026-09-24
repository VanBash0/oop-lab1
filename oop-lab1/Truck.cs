using oop_lab1.Results;

namespace oop_lab1;

public class Truck : SkuHolder
{
    private readonly double _speed;
    
    public Coordinates Location { get; private set; }

    public Truck(double сapacity, double speed, Coordinates location) : base(сapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(speed);
        _speed = speed;
        Location = location;
    }

    public TimeSpan MoveTo(Coordinates target)
    {
        var distance = DistanceCalculator.Distance(Location, target);
        var hours = distance / _speed;
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
