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
