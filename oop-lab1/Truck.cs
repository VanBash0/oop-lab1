using oop_lab1.Results;

namespace oop_lab1;

public class Truck
{
    public double Capacity { get; }
    public double Speed { get; }
    public Coordinates Location { get; private set; }
    private readonly Dictionary<Sku, uint> _skus;

    public Truck(double capacity, double speed, Coordinates location)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(speed);
        Capacity = capacity;
        Speed = speed;
        Location = location;
        _skus = new Dictionary<Sku, uint>();
    }

    public TimeSpan MoveTo(Coordinates target)
    {
        var distance = DistanceCalculator.Distance(Location, target);
        var hours = distance / Speed;
        Location = target;
        return TimeSpan.FromHours(hours);
    }

    private double GetTotalVolumeWeightChars()
    {
        double total = 0;
        foreach (var sku in _skus)
        {
            total += sku.Key.VolumeWeightChars * sku.Value;
        }
        return total;
    }

    public TruckLoadResult Load(Sku sku, uint quantity)
    {
        if (sku.VolumeWeightChars * quantity > Capacity - GetTotalVolumeWeightChars())
        {
            return new TruckLoadResult.LoadFailure((sku, quantity));
        }

        if (!_skus.ContainsKey(sku))
        {
            _skus.Add(sku, quantity);
        }
        else
        {
            _skus[sku] += quantity;
        }

        return new TruckLoadResult.LoadSuccess();
    }

    public TruckUnloadResult Unload(Sku sku, uint quantity)
    {
        if (!_skus.ContainsKey(sku) || _skus[sku] < quantity)
        {
            return new TruckUnloadResult.UnloadFailure((sku, quantity));
        }
        
        _skus[sku] -= quantity;
        if (_skus[sku] == 0)
        {
            _skus.Remove(sku);
        }
        
        return new TruckUnloadResult.UnloadSuccess();
    }
}
