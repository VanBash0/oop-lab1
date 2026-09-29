namespace oop_lab1.SKU;

public readonly record struct SkuSet(Sku Sku, int Quantity)
{
    public VolumeWeightChars TotalVolumeWeight => Sku.VolumeWeightChars.Multiply(Quantity);
}