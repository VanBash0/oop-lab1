namespace oop_lab1;

public readonly record struct SkuId(uint Value);

public class Sku
{
    public SkuId Id { get; }
    public string Name { get; }
    public double VolumeWeightChars { get; }

    public Sku(SkuId id, string name, double volumeWeightChars)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(volumeWeightChars);
        Id = id;
        Name = name;
        VolumeWeightChars = volumeWeightChars;
    }
}

public readonly record struct SkuSet(Sku Sku, uint Quantity);