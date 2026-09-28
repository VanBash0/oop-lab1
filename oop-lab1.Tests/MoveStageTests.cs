using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.Route;
using oop_lab1.SKU;

namespace oop_lab1.Tests;

public class MoveStageTests
{
    [Fact]
    public void Execute_ValidCoordinates_CorrectTime()
    {
        var from = new Coordinates(0, 0);
        var to = new Coordinates(0, 1);
        const double speed = 10.0;
        
        const double earthRadius = 6371.0;
        var expectedDistance = earthRadius * Math.PI / 180;
        
        var truck = new Truck(new VolumeWeightChars(1.0, 1.0), speed, from);
        var moveStage = new MoveStage(to);

        var result = Assert.IsType<RouteStageResult.StageSuccess>(moveStage.Execute(truck));
        var actualTime = result.TotalTime;
        var expectedTime = TimeSpan.FromHours(expectedDistance / speed);
        var tolerance = TimeSpan.FromSeconds(5);

        Assert.InRange(actualTime, expectedTime - tolerance, expectedTime + tolerance);
        Assert.Equal(to, truck.Location);
    }
}