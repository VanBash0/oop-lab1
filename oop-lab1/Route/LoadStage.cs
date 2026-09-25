using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.SKU;

namespace oop_lab1.Route;

public class LoadStage : WarehouseStage
{
    public LoadStage(Warehouse warehouse, Manifest manifest) : base(warehouse, manifest) { }

    public override RouteStageResult Execute(Truck truck)
    {
        var totalTime = TimeSpan.Zero;
        
        if (!IsTruckAtWarehouse(truck))
        {
            return new RouteStageResult.TruckTooFar(_warehouse);
        }

        var unloadResult = _warehouse.Unload(_manifest);
        switch (unloadResult)
        {
            case WarehouseUnloadResult.UnloadFailure:
                return new RouteStageResult.WarehouseInsufficientStock(_manifest);
            case WarehouseUnloadResult.UnloadSuccess success:
                totalTime += success.UnloadTime;
                break;
        }
        
        var loadResult = truck.Load(_manifest);
        if (loadResult is TruckLoadResult.LoadFailure loadFailure)
        {
            return new RouteStageResult.TruckInsufficientCapacity(loadFailure.RejectedManifest);
        }

        return new RouteStageResult.StageSuccess(totalTime);
    }
}