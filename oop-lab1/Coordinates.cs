namespace oop_lab1;

public readonly struct Coordinates
{
    public double Latitude { get; }
    public double Longitude { get; }
    
    public Coordinates(double latitude, double longitude)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(latitude, -90);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(latitude, 90);
        ArgumentOutOfRangeException.ThrowIfLessThan(longitude, -180);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(longitude, 180);
        
        Latitude = latitude;
        Longitude = longitude;
    }
}
