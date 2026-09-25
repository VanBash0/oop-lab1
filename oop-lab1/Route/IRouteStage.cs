using oop_lab1.Logistics;
using oop_lab1.Results;

namespace oop_lab1.Route;

public interface IRouteStage
{
    public RouteStageResult Execute(Truck truck);
}