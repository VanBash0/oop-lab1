namespace oop_lab1;

public static class DistanceCalculator
{
    public static double Distance(Coordinates from, Coordinates to)
    {
        return Math.Sqrt((from.Latitude - to.Latitude) * (from.Latitude - to.Latitude) +
                         (from.Longitude - to.Longitude) * (from.Longitude - to.Longitude));
    }
}