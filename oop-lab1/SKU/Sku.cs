namespace oop_lab1.SKU;

public readonly record struct SkuId(int Value);

public class Sku
{
    public SkuId Id { get; }
    public string Name { get; }
    public VolumeWeightChars VolumeWeightChars { get; }

    public Sku(SkuId id, string name, VolumeWeightChars volumeWeightChars)
    {
        if (volumeWeightChars.IsEmpty)
        {
            throw new ArgumentOutOfRangeException(volumeWeightChars.ToString());
        }
        Id = id;
        Name = name;
        VolumeWeightChars = volumeWeightChars;
    }
}

public readonly record struct SkuSet(Sku Sku, uint Quantity);