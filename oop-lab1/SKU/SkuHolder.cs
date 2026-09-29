namespace oop_lab1.SKU;

public abstract class SkuHolder
{
    private readonly VolumeWeightChars _capacity;
    private readonly Dictionary<Sku, int> _skus;

    protected SkuHolder(VolumeWeightChars capacity)
    {
        _capacity = capacity;
        _skus = new Dictionary<Sku, int>();
    }
    
    private VolumeWeightChars GetFreeSpace()
    {
        var occupiedChars = new VolumeWeightChars();
        foreach (var sku in _skus)
        {
            var unit =  sku.Key.VolumeWeightChars;
            occupiedChars = occupiedChars.Add(unit.Multiply(sku.Value));
        }

        return _capacity.Subtract(occupiedChars);
    }

    protected bool TryAddSku(Manifest manifest)
    {
        if (!manifest.GetTotalVolumeWeightChars().FitsIn(GetFreeSpace()))
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
            if (!_skus.ContainsKey(skuSet.Sku))
            {
                return false;
            }
            
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
