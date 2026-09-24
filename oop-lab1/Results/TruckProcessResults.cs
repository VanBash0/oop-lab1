namespace oop_lab1.Results;

public abstract record TruckLoadResult
{
    private TruckLoadResult() { }
    
    public record LoadSuccess : TruckLoadResult;
    
    public record LoadFailure(Manifest RejectedManifest) : TruckLoadResult;
}

public abstract record TruckUnloadResult
{
    private TruckUnloadResult() { }
    
    public record UnloadSuccess : TruckUnloadResult;
    
    public record UnloadFailure(Manifest RejectedManifest) : TruckUnloadResult;
}