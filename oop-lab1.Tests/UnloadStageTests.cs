using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.Route;
using oop_lab1.SKU;
using oop_lab1.Workforce;

namespace oop_lab1.Tests;

public class UnloadStageTests
{
    [Fact]
    public void Execute_ValidStage_Success()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        truck.Load(manifest);

        var unloadStage = new UnloadStage(warehouse, manifest);
        var result = Assert.IsType<RouteStageResult.StageSuccess>(unloadStage.Execute(truck));
        Assert.Equal(TimeSpan.FromMinutes(5), result.TotalTime);
    }

    [Fact]
    public void Execute_TruckTooFar_Failure()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0),  10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new  WarehouseId(1), new Coordinates(80.0, 80.0),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        truck.Load(manifest);
        
        var unloadStage = new UnloadStage(warehouse, manifest);
        Assert.IsType<RouteStageResult.TruckTooFar>(unloadStage.Execute(truck));
    }

    [Fact]
    public void Execute_TruckInsufficientStock_Failure()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0),  10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new  WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var bigSet = new SkuSet(laptop, 50);
        
        var loadManifest = new Manifest(new List<SkuSet>() { laptopSet });
        truck.Load(loadManifest);
        
        var unloadManifest = new Manifest(new List<SkuSet>() { bigSet });
        var unloadStage = new UnloadStage(warehouse, unloadManifest);
        Assert.IsType<RouteStageResult.TruckInsufficientStock>(unloadStage.Execute(truck));
    }

    [Fact]
    public void Execute_WarehouseInsufficientCapacity_Failure()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(10.0, 10.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);

        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        truck.Load(manifest);

        var unloadStage = new UnloadStage(warehouse, manifest);
        Assert.IsType<RouteStageResult.WarehouseInsufficientCapacity>(unloadStage.Execute(truck));
    }
}