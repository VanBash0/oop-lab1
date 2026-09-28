using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.Route;
using oop_lab1.SKU;
using oop_lab1.Workforce;

namespace oop_lab1.Tests;

public class RouteSheetTests
{
    [Fact]
    public void Run_ValidStages_TotalTimeIsSumOfStageTimes()
    {
        var from = new Coordinates(0, 0);
        var to = new Coordinates(0, 1);
        const double speed = 10.0;

        const double earthRadius = 6371.0;
        var expectedDistance = earthRadius * Math.PI / 180;

        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), speed, from);
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var sourceWarehouse = new Warehouse(new WarehouseId(1), from,
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        var destinationWarehouse = new Warehouse(new WarehouseId(2), to,
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        sourceWarehouse.Load(manifest);

        var routeSheet = new RouteSheet(new List<IRouteStage>()
        {
            new LoadStage(sourceWarehouse, manifest),
            new MoveStage(to),
            new UnloadStage(destinationWarehouse, manifest)
        });

        var result = Assert.IsType<RouteStageResult.StageSuccess>(routeSheet.Run(truck));

        var expectedTime = TimeSpan.FromMinutes(6)
                           + TimeSpan.FromHours(expectedDistance / speed)
                           + TimeSpan.FromMinutes(5);
        var tolerance = TimeSpan.FromSeconds(5);

        Assert.InRange(result.TotalTime, expectedTime - tolerance, expectedTime + tolerance);
        Assert.Equal(to, truck.Location);
    }

    [Fact]
    public void Run_FirstStageFails_ReturnsErrorAtLoading()
    {
        var from = new Coordinates(0, 0);
        var to = new Coordinates(0, 1);

        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), 10.0, from);
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var sourceWarehouse = new Warehouse(new WarehouseId(1), from,
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        var destinationWarehouse = new Warehouse(new WarehouseId(2), to,
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        var routeSheet = new RouteSheet(new List<IRouteStage>()
        {
            new LoadStage(sourceWarehouse, manifest),
            new MoveStage(to),
            new UnloadStage(destinationWarehouse, manifest)
        });

        Assert.IsType<RouteStageResult.WarehouseInsufficientStock>(routeSheet.Run(truck));
    }

    [Fact]
    public void Run_LastStageFails_ReturnsErrorAtUnloading()
    {
        var from = new Coordinates(0, 0);
        var to = new Coordinates(0, 1);

        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), 10.0, from);
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var sourceWarehouse = new Warehouse(new WarehouseId(1), from,
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        var destinationWarehouse = new Warehouse(new WarehouseId(2), to,
            new VolumeWeightChars(10.0, 10.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        sourceWarehouse.Load(manifest);

        var routeSheet = new RouteSheet(new List<IRouteStage>()
        {
            new LoadStage(sourceWarehouse, manifest),
            new MoveStage(to),
            new UnloadStage(destinationWarehouse, manifest)
        });

        Assert.IsType<RouteStageResult.WarehouseInsufficientCapacity>(routeSheet.Run(truck));
    }
}