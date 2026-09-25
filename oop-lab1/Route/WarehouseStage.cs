using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.SKU;

namespace oop_lab1.Route;

public abstract class WarehouseStage : IRouteStage
{
    protected readonly Warehouse _warehouse;
    protected readonly Manifest _manifest;

    protected WarehouseStage(Warehouse warehouse, Manifest manifest)
    {
        _warehouse = warehouse;
        _manifest = manifest;
    }

    public abstract RouteStageResult Execute(Truck truck);
    
    private const double TruckRange = 0.01;

    protected bool IsTruckAtWarehouse(Truck truck)
    {
        return DistanceCalculator.Distance(truck.Location, _warehouse.Location) <= TruckRange;
    }
}