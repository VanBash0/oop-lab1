using oop_lab1.Logistics;
using oop_lab1.Results;

namespace oop_lab1.Route;

public class RouteSheet
{
    private readonly List<IRouteStage> _stages;

    public RouteSheet(IEnumerable<IRouteStage> stages)
    {
        _stages = stages.ToList();
    }

    public RouteStageResult Run(Truck truck)
    {
        var totalTime = TimeSpan.Zero;
        foreach (var stage in _stages)
        {
            var result = stage.Execute(truck);
            if (result is RouteStageResult.StageSuccess success)
            {
                totalTime += success.TotalTime;
            }
            else
            {
                return result;
            }
        }
        
        return new RouteStageResult.StageSuccess(totalTime);
    }
}