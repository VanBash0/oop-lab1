namespace oop_lab1;

public abstract class SkuHolder
{
    private readonly double _сapacity;
    private readonly Dictionary<Sku, uint> _skus;

    protected SkuHolder(double сapacity)
    {
        _сapacity = сapacity;
        _skus = new Dictionary<Sku, uint>();
    }
    
    private double GetFreeSpace()
    {
        double occupiedSpace = 0;
        foreach (var sku in _skus)
        {
            occupiedSpace += sku.Key.VolumeWeightChars * sku.Value;
        }
        return _сapacity - occupiedSpace;
    }

    protected bool TryAddSku(Manifest manifest)
    {
        if (manifest.GetTotalVolumeWeightChars() > GetFreeSpace())
        {
            return false;
        }

        foreach (var skuSet in manifest.SkuSets)
        {
            var sku = skuSet.Sku;
            var quantity = skuSet.Quantity;
            if (!_skus.ContainsKey(sku))
            {
                _skus.Add(sku, quantity);
            }
            else
            {
                _skus[sku] += quantity;
            }
        }
        
        return true;
    }

    protected bool TryRemoveSku(Manifest manifest)
    {
        foreach (var skuSet in manifest.SkuSets)
        {
            if (!_skus.ContainsKey(skuSet.Sku) || _skus[skuSet.Sku] < skuSet.Quantity)
            {
                return false;
            }
        }

        foreach (var skuSet in manifest.SkuSets)
        {
            var sku = skuSet.Sku;
            var quantity = skuSet.Quantity;
            _skus[sku] -= quantity;
            if (_skus[sku] == 0)
            {
                _skus.Remove(sku);
            }
        }

        return true;
    }
}
