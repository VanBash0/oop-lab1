using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.Route;
using oop_lab1.SKU;
using oop_lab1.Workforce;

namespace oop_lab1.Tests;

public class LoadStageTests
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
        warehouse.Load(manifest);

        var loadStage = new LoadStage(warehouse, manifest);
        var result = Assert.IsType<RouteStageResult.StageSuccess>(loadStage.Execute(truck));
        Assert.Equal(TimeSpan.FromMinutes(6), result.TotalTime);
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
        warehouse.Load(manifest);
        
        var loadStage = new LoadStage(warehouse, manifest);
        Assert.IsType<RouteStageResult.TruckTooFar>(loadStage.Execute(truck));
    }

    [Fact]
    public void Execute_WarehouseInsufficientStock_Failure()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0),  10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new  WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });
        
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var bigSet = new SkuSet(laptop, 50);
        
        var warehouseManifest = new Manifest(new List<SkuSet>() { laptopSet });
        warehouse.Load(warehouseManifest);
        
        var loadManifest = new Manifest(new List<SkuSet>() { bigSet });
        var loadStage = new LoadStage(warehouse, loadManifest);
        Assert.IsType<RouteStageResult.WarehouseInsufficientStock>(loadStage.Execute(truck));
    }

    [Fact]
    public void Execute_TruckInsufficientCapacity_Failure()
    {
        var truck = new Truck(new VolumeWeightChars(10.0, 10.0), 10.0, new Coordinates());
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);

        var manifest = new Manifest(new List<SkuSet>() { laptopSet });
        warehouse.Load(manifest);

        var loadStage = new LoadStage(warehouse, manifest);
        Assert.IsType<RouteStageResult.TruckInsufficientCapacity>(loadStage.Execute(truck));
    }
}