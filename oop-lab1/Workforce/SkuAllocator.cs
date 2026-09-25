using oop_lab1.SKU;

namespace oop_lab1.Workforce;

public static class SkuAllocator
{
    public static bool TryLoadEmployee(Employee employee, List<SkuSet> remainingSkus)
    {
        var freeCapacity = employee.Capacity;
        var isLoaded = false;

        var index = 0;
        while (index < remainingSkus.Count && freeCapacity > 0)
        {
            var skuSet = remainingSkus[index];
            var fittingCount = (uint)Math.Min(Math.Floor(freeCapacity / skuSet.Sku.VolumeWeightChars), skuSet.Quantity);

            if (fittingCount == 0)
            {
                index++;
                continue;
            }

            freeCapacity -= skuSet.Sku.VolumeWeightChars * fittingCount;
            isLoaded = true;
            if (fittingCount == skuSet.Quantity)
            {
                remainingSkus.RemoveAt(index);
            }
            else
            {
                remainingSkus[index] = skuSet with { Quantity = skuSet.Quantity - fittingCount };
                index++;
            }
        }

        return isLoaded;
    }
}