using oop_lab1.SKU;

namespace oop_lab1.Tests;

public class SkuTests
{
    [Fact]
    public void SkuConstruct_ValidArguments_Success()
    {
        var sku = new Sku(new SkuId(1), "laptop", new VolumeWeightChars(10.0, 5.0));
        Assert.Equal("laptop", sku.Name);
        Assert.Equal(new SkuId(1), sku.Id);
        Assert.Equal(new VolumeWeightChars(10.0, 5.0), sku.VolumeWeightChars);
    }

    [Theory]
    [InlineData(-10.0, -5.0)]
    [InlineData(-10.0, 5.0)]
    [InlineData(10.0, -5.0)]
    [InlineData(0.0, 5.0)]
    [InlineData(10.0, 0.0)]
    [InlineData(0.0, 0.0)]
    public void SkuConstruct_InvalidVolumeWeightChars_ThrowsException(double volume, double weight)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Sku(new SkuId(1), "laptop", new VolumeWeightChars(volume, weight))
        );
    }
}