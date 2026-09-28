using oop_lab1.SKU;
using oop_lab1.Geography;
using oop_lab1.Logistics;
using oop_lab1.Route;
using oop_lab1.Workforce;

namespace oop_lab1;

public class Program
{
    public static void Main()
    {
        var mskCoords = new Coordinates(55.7558, 37.6173);
        var spbCoords = new Coordinates(59.9343, 30.3351);
        
        Console.WriteLine(DistanceCalculator.Distance(mskCoords, spbCoords));
    }
}