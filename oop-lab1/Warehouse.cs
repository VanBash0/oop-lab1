using oop_lab1.Results;

namespace oop_lab1;

public readonly struct WarehouseId(uint Value);

public class Warehouse : SkuHolder
{
    private WarehouseId Id { get; }
    private Coordinates Location { get; }
    
    private readonly EmployeeManager _manager;

    public Warehouse(WarehouseId id, Coordinates location,
                     uint capacity, List<Employee> employees) : base(capacity)
    {
        Id = id;
        Location = location;
        _manager = new EmployeeManager(employees);
    }

    public WarehouseLoadResult Load(Sku sku, uint quantity)
    {
        if (!TryAddSku(sku, quantity))
        {
            return new WarehouseLoadResult.LoadFailure((sku, quantity));
        }
        
        var loadTime = _manager.CalculateLoadTime(sku, quantity);
        return new WarehouseLoadResult.LoadSuccess(loadTime);
    }

    public WarehouseUnloadResult Unload(Sku sku, uint quantity)
    {
        if (!TryRemoveSku(sku, quantity))
        {
            return new WarehouseUnloadResult.UnloadFailure((sku, quantity));
        }
        
        var unloadTime = _manager.CalculateUnloadTime(sku, quantity);
        return new WarehouseUnloadResult.UnloadSuccess(unloadTime);
    }
}
