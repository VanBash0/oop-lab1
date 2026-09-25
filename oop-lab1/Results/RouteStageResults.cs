using oop_lab1.Logistics;
using oop_lab1.SKU;

namespace oop_lab1.Results;

public record RouteStageResult
{
    private RouteStageResult() { }

    public sealed record TruckInsufficientCapacity(Manifest RejectedManifest) : RouteStageResult;
    
    public sealed record TruckInsufficientStock(Manifest RejectedManifest) : RouteStageResult;
    
    public sealed record WarehouseInsufficientCapacity(Manifest RejectedManifest) : RouteStageResult;
    
    public sealed record WarehouseInsufficientStock(Manifest RejectedManifest) : RouteStageResult;
    
    public sealed record TruckTooFar(Warehouse Warehouse) : RouteStageResult;

    public sealed record StageSuccess(TimeSpan TotalTime) : RouteStageResult;
}