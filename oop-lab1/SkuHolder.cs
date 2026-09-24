namespace oop_lab1;

public class SkuHolder
{
    private double Capacity { get; }
    
    private readonly Dictionary<Sku, uint> _skus;

    protected SkuHolder(double capacity)
    {
        Capacity = capacity;
        _skus = new Dictionary<Sku, uint>();
    }
    
    private double GetFreeSpace()
    {
        double occupiedSpace = 0;
        foreach (var sku in _skus)
        {
            occupiedSpace += sku.Key.VolumeWeightChars * sku.Value;
        }
        return Capacity - occupiedSpace;
    }

    protected bool TryAddSku(Sku sku, uint quantity)
    {
        if (sku.VolumeWeightChars * quantity > GetFreeSpace())
        {
            return false;
        }
        
        if (!_skus.ContainsKey(sku))
        {
            _skus.Add(sku, quantity);
        }
        else
        {
            _skus[sku] += quantity;
        }

        return true;
    }

    protected bool TryRemoveSku(Sku sku, uint quantity)
    {
        if (!_skus.ContainsKey(sku) || _skus[sku] < quantity)
        {
            return false;
        }
        
        _skus[sku] -= quantity;
        if (_skus[sku] == 0)
        {
            _skus.Remove(sku);
        }

        return true;
    }
}
