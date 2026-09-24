namespace oop_lab1.Results;

public abstract record TruckLoadResult
{
    private TruckLoadResult() { }
    
    public sealed record LoadSuccess : TruckLoadResult;
    
    public sealed record LoadFailure(Manifest RejectedManifest) : TruckLoadResult;
}

public abstract record TruckUnloadResult
{
    private TruckUnloadResult() { }
    
    public sealed record UnloadSuccess : TruckUnloadResult;
    
    public sealed record UnloadFailure(Manifest RejectedManifest) : TruckUnloadResult;
}