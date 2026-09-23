namespace oop_lab1;

public class Sku
{
    public int Id { get; }
    public string Name { get; }
    public double VolumeWeightChars { get; }

    public Sku(int id, string name, double volumeWeightChars)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(volumeWeightChars);
        Id = id;
        Name = name;
        VolumeWeightChars = volumeWeightChars;
    }
}