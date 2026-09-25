using oop_lab1.Geography;
using oop_lab1.Results;
using oop_lab1.SKU;
using oop_lab1.Workforce;

namespace oop_lab1.Logistics;

public readonly record struct WarehouseId(uint Value);

public class Warehouse : SkuHolder
{
    private readonly WarehouseId _id;
    public Coordinates Location { get; }
    
    private readonly EmployeeManager _manager;

    public Warehouse(WarehouseId id, Coordinates location,
                     uint capacity, List<Employee> employees) : base(capacity)
    {
        _id = id;
        Location = location;
        _manager = new EmployeeManager(employees);
    }

    public WarehouseLoadResult Load(Manifest manifest)
    {
        if (!TryAddSku(manifest))
        {
            return new WarehouseLoadResult.LoadFailure(manifest);
        }
        
        var loadTime = _manager.CalculateLoadTime(manifest);
        return new WarehouseLoadResult.LoadSuccess(loadTime);
    }

    public WarehouseUnloadResult Unload(Manifest manifest)
    {
        if (!TryRemoveSku(manifest))
        {
            return new WarehouseUnloadResult.UnloadFailure(manifest);
        }
        
        var unloadTime = _manager.CalculateUnloadTime(manifest);
        return new WarehouseUnloadResult.UnloadSuccess(unloadTime);
    }
}
