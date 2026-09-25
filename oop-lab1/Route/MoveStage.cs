using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;

namespace oop_lab1.Route;

public class MoveStage : IRouteStage
{
    private readonly Coordinates _target;
    
    public MoveStage(Coordinates target)
    {
        _target = target;
    }
    
    public RouteStageResult Execute(Truck truck)
    {
        var travelTime = truck.MoveTo(_target);
        return new RouteStageResult.StageSuccess(travelTime);
    }
}