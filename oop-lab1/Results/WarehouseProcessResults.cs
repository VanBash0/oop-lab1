namespace oop_lab1.Results;

public abstract record WarehouseLoadResult
{
    private WarehouseLoadResult() { }
    
    public record LoadSuccess(TimeSpan LoadTime) : WarehouseLoadResult;
    
    public record LoadFailure(Manifest RejectedManifest) : WarehouseLoadResult;
}

public abstract record WarehouseUnloadResult
{
    private WarehouseUnloadResult() { }
    
    public record UnloadSuccess(TimeSpan UnloadTIme) : WarehouseUnloadResult;
    
    public record UnloadFailure(Manifest RejectedManifest) : WarehouseUnloadResult;
}
