namespace oop_lab1.Results;

public abstract record TruckLoadResult
{
    public record LoadSuccess : TruckLoadResult;
    
    public record LoadFailure((Sku, uint) RejectedSku) : TruckLoadResult;
}

public abstract record TruckUnloadResult
{
    public record UnloadSuccess : TruckUnloadResult;
    
    public record UnloadFailure((Sku, uint) RejectedSku) : TruckUnloadResult;
}