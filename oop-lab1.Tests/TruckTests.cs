using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Results;
using oop_lab1.SKU;

namespace oop_lab1.Tests;

public class TruckTests
{
    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    public void Construct_InvalidSpeed_ThrowsException(double speed)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Truck(new VolumeWeightChars(1.0, 1.0), speed, new Coordinates())
        );
    }

    [Fact]
    public void Load_WithinCapacityLimits_Success()
    {
        var truck = new Truck(new VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
        
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        var skuSet = new SkuSet(laptop, 5);
        var manifest = new Manifest(new List<SkuSet> { skuSet });
        
        var loadResult = truck.Load(manifest);
        
        Assert.IsType<TruckLoadResult.LoadSuccess>(truck.Load(manifest));
    }

    [Fact]
    public void Load_EmptyTruckOutOfCapacityLimits_Fail()
    {
        var truck = new Truck(new  VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
            
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        var laptopSet = new SkuSet(laptop, 50);
        
        var manifest = new Manifest(new List<SkuSet> { laptopSet });
        Assert.IsType<TruckLoadResult.LoadFailure>(truck.Load(manifest));
    }
    
    [Fact]
    public void Load_LoadedTruckOutOfCapacityLimits_Failure()
    {
        var truck = new Truck(new  VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
            
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        var laptopSet = new SkuSet(laptop, 5);
        
        var tv = new Sku(new SkuId(2), "tv", new VolumeWeightChars(90.0, 80.0));
        var tvSet = new SkuSet(tv, 1);
            
        var laptopManifest = new Manifest(new List<SkuSet> { laptopSet });
        var tvManifest = new Manifest(new List<SkuSet> { tvSet });
        
        truck.Load(laptopManifest);
        Assert.IsType<TruckLoadResult.LoadFailure>(truck.Load(tvManifest));
    }

    [Fact]
    public void Unload_TruckContainsManifest_Success()
    {
        var truck = new Truck(new  VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
            
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        var loadSet = new SkuSet(laptop, 5);
        var unloadSet = new SkuSet(laptop, 1);
        
        var loadManifest = new Manifest(new List<SkuSet> { loadSet });
        var unloadManifest = new Manifest(new List<SkuSet> { unloadSet });
        
        truck.Load(loadManifest);
        var unloadResult = truck.Unload(unloadManifest);
        Assert.IsType<TruckUnloadResult.UnloadSuccess>(unloadResult);
    }

    [Fact]
    public void Unload_TruckDoesNotContainManifest_Failure()
    {
        var truck = new Truck(new  VolumeWeightChars(100.0, 100.0), 10.0, new Coordinates());
            
        var laptop = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        var unloadSet = new SkuSet(laptop, 5);
        var loadSet = new SkuSet(laptop, 1);
        
        var loadManifest = new Manifest(new List<SkuSet> { loadSet });
        var unloadManifest = new Manifest(new List<SkuSet> { unloadSet });
        
        truck.Load(loadManifest);
        Assert.IsType<TruckUnloadResult.UnloadFailure>(truck.Unload(unloadManifest));
    }

    [Fact]
    public void MoveTo_Coordinates_Time()
    {
        var from = new Coordinates(0, 0);
        var to = new Coordinates(0, 1);
        const double speed = 10.0;
        
        const double earthRadius = 6371.0;
        var expectedDistance = earthRadius * Math.PI / 180;
        
        var truck = new Truck(new VolumeWeightChars(1.0, 1.0), speed, from);
        var actualTime = truck.MoveTo(to);
        var expectedTime = TimeSpan.FromHours(expectedDistance / speed);
        var tolerance = TimeSpan.FromSeconds(5);

        Assert.InRange(actualTime, expectedTime - tolerance, expectedTime + tolerance);
        Assert.Equal(to, truck.Location);
    }
}