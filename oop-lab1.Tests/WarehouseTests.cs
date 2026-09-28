using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.SKU;
using oop_lab1.Workforce;

namespace oop_lab1.Tests;

public class WarehouseTests
{
    [Fact]
    public void Load_WithinFreeCapacity_Success()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        Assert.IsType<WarehouseLoadResult.LoadSuccess>(warehouse.Load(manifest));
    }

    [Fact]
    public void Load_OutOfCapacityLimits_Failure()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 11);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        Assert.IsType<WarehouseLoadResult.LoadFailure>(warehouse.Load(manifest));
    }

    [Fact]
    public void Unload_WarehouseContainsManifest_Success()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var loadSet = new SkuSet(laptop, 5);
        var unloadSet = new SkuSet(laptop, 2);

        var loadManifest = new Manifest(new List<SkuSet>() { loadSet });
        var unloadManifest = new Manifest(new List<SkuSet>() { unloadSet });

        warehouse.Load(loadManifest);
        Assert.IsType<WarehouseUnloadResult.UnloadSuccess>(warehouse.Unload(unloadManifest));
    }

    [Fact]
    public void Unload_QuantityExceedsStock_Failure()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var loadSet = new SkuSet(laptop, 1);
        var unloadSet = new SkuSet(laptop, 5);

        var loadManifest = new Manifest(new List<SkuSet>() { loadSet });
        var unloadManifest = new Manifest(new List<SkuSet>() { unloadSet });

        warehouse.Load(loadManifest);
        Assert.IsType<WarehouseUnloadResult.UnloadFailure>(warehouse.Unload(unloadManifest));
    }

    [Fact]
    public void Load_SingleEmployee_CorrectTime()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        var result = Assert.IsType<WarehouseLoadResult.LoadSuccess>(warehouse.Load(manifest));
        Assert.Equal(TimeSpan.FromMinutes(5), result.LoadTime);
    }

    [Fact]
    public void Load_MultipleEmployees_CorrectTime()
    {
        var firstEmployee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var secondEmployee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { firstEmployee, secondEmployee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        var result = Assert.IsType<WarehouseLoadResult.LoadSuccess>(warehouse.Load(manifest));
        Assert.Equal(TimeSpan.FromMinutes(3), result.LoadTime);
    }

    [Fact]
    public void Unload_SingleEmployee_CorrectTime()
    {
        var employee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { employee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        warehouse.Load(manifest);
        var result = Assert.IsType<WarehouseUnloadResult.UnloadSuccess>(warehouse.Unload(manifest));
        Assert.Equal(TimeSpan.FromMinutes(6), result.UnloadTime);
    }

    [Fact]
    public void Unload_MultipleEmployees_CorrectTime()
    {
        var firstEmployee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var secondEmployee = new Employee(new VolumeWeightChars(20.0, 20.0),
            TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        var warehouse = new Warehouse(new WarehouseId(1), new Coordinates(),
            new VolumeWeightChars(100.0, 100.0), new List<Employee>() { firstEmployee, secondEmployee });

        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 10.0));
        var laptopSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet>() { laptopSet });

        warehouse.Load(manifest);
        var result = Assert.IsType<WarehouseUnloadResult.UnloadSuccess>(warehouse.Unload(manifest));
        Assert.Equal(TimeSpan.FromMinutes(4), result.UnloadTime);
    }
}