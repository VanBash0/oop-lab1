namespace oop_lab1.SKU;

public class Manifest
{
    private readonly List<SkuSet> _skuSets;

    public Manifest(IEnumerable<SkuSet> skuSets)
    {
        _skuSets = skuSets.OrderByDescending(skuSet => skuSet.Sku.VolumeWeightChars).ToList();
    }
    
    public IReadOnlyList<SkuSet> SkuSets => _skuSets;

    public VolumeWeightChars GetTotalVolumeWeightChars()
    {
        var totalChars = new VolumeWeightChars();
        foreach (var skuSet in _skuSets)
        {
            var unit = skuSet.Sku.VolumeWeightChars;
            totalChars = totalChars.Add(unit.Multiply(skuSet.Quantity));
        }

        return totalChars;
    }
}
