using oop_lab1.SKU;

namespace oop_lab1.Results;

public abstract record WarehouseLoadResult
{
    private WarehouseLoadResult() { }
    
    public sealed record LoadSuccess(TimeSpan LoadTime) : WarehouseLoadResult;
    
    public sealed record LoadFailure(Manifest RejectedManifest) : WarehouseLoadResult;
}

public abstract record WarehouseUnloadResult
{
    private WarehouseUnloadResult() { }
    
    public sealed record UnloadSuccess(TimeSpan UnloadTIme) : WarehouseUnloadResult;
    
    public sealed record UnloadFailure(Manifest RejectedManifest) : WarehouseUnloadResult;
}