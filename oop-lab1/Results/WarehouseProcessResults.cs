namespace oop_lab1.Results;

public abstract record WarehouseLoadResult
{
    private WarehouseLoadResult() { }
    
    public record LoadSuccess(TimeSpan LoadTime) : WarehouseLoadResult;
    
    public record LoadFailure((Sku, uint) RejectedSku) : WarehouseLoadResult;
}

public abstract record WarehouseUnloadResult
{
    private WarehouseUnloadResult() { }
    
    public record UnloadSuccess(TimeSpan UnloadTIme) : WarehouseUnloadResult;
    
    public record UnloadFailure((Sku, uint) RejectedSku) : WarehouseUnloadResult;
}
