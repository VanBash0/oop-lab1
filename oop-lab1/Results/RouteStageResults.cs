using oop_lab1.Logistics;
using oop_lab1.SKU;

namespace oop_lab1.Results;

public record RouteStageResult
{
    private RouteStageResult() { }

    public sealed record TruckInsufficientCapacity : RouteStageResult;
    
    public sealed record TruckInsufficientStock : RouteStageResult;
    
    public sealed record WarehouseInsufficientCapacity : RouteStageResult;
    
    public sealed record WarehouseInsufficientStock : RouteStageResult;
    
    public sealed record TruckTooFar : RouteStageResult;

    public sealed record StageSuccess(TimeSpan TotalTime) : RouteStageResult;
}