namespace oop_lab1.Geography;

public static class DistanceCalculator
{
    private const int EarthRadiusInKm = 6371;
    
    private static double ToRadians(double degrees) => degrees * Math.PI / 180;
    
    public static double Distance(Coordinates from, Coordinates to)
    {
        var phi1 = ToRadians(from.Latitude);
        var phi2 = ToRadians(to.Latitude);
        
        var lambda1 = ToRadians(from.Longitude);
        var lambda2 = ToRadians(to.Longitude);

        return 2 * EarthRadiusInKm * Math.Asin(Math.Sqrt(
            Math.Sin((phi2 - phi1) / 2) * Math.Sin((phi2 - phi1) / 2) +
            Math.Cos(phi1) * Math.Cos(phi2) * Math.Sin((lambda2 - lambda1) / 2) * Math.Sin((lambda2 - lambda1) / 2)
        ));
    }
}