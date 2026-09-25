namespace oop_lab1.SKU;

public class Manifest
{
    private readonly List<SkuSet> _skuSets;

    public Manifest(IEnumerable<SkuSet> skuSets)
    {
        _skuSets = skuSets.OrderByDescending(skuSet => skuSet.Sku.VolumeWeightChars).ToList();
    }
    
    public IReadOnlyList<SkuSet> SkuSets => _skuSets;

    public double GetTotalVolumeWeightChars()
    {
        var total = 0.0;
        foreach (var skuSet in _skuSets)
        {
            total += skuSet.Sku.VolumeWeightChars * skuSet.Quantity;
        }

        return total;
    }
}
