using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.SKU;

namespace oop_lab1.Route;

public class UnloadStage : WarehouseStage
{
    public UnloadStage(Warehouse warehouse, Manifest manifest)  : base(warehouse, manifest) {}

    public override RouteStageResult Execute(Truck truck)
    {
        var totalTime = TimeSpan.Zero;
        
        if (!IsTruckAtWarehouse(truck))
        {
            return new RouteStageResult.TruckTooFar(_warehouse);
        }
        
        var unloadResult = truck.Unload(_manifest);
        if (unloadResult is TruckUnloadResult.UnloadFailure unloadFailure)
        {
            return new RouteStageResult.TruckInsufficientStock(_manifest);
        }
        
        var loadResult = _warehouse.Load(_manifest);
        switch (loadResult)
        {
            case WarehouseLoadResult.LoadFailure:
                return new RouteStageResult.WarehouseInsufficientCapacity(_manifest);
            case WarehouseLoadResult.LoadSuccess success:
                totalTime += success.LoadTime;
                break;
        }
        
        return new RouteStageResult.StageSuccess(totalTime);
    }
}