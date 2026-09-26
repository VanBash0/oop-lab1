namespace oop_lab1.SKU;

public readonly struct VolumeWeightChars : IComparable<VolumeWeightChars>
{
    private readonly double _volume;
    private readonly double _weight;
    
    public VolumeWeightChars(double volume, double weight)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(volume);
        ArgumentOutOfRangeException.ThrowIfNegative(weight);
        _volume = volume;
        _weight = weight;
    }

    public bool FitsIn(VolumeWeightChars other)
    {
        return _volume <= other._volume && _weight <= other._weight;
    }

    public uint GetMaxFitIn(VolumeWeightChars available)
    {
        return (uint)Math.Min(Math.Floor(available._volume / _volume), Math.Floor(available._weight / _weight));
    }

    public VolumeWeightChars Subtract(VolumeWeightChars other)
    {
        return new VolumeWeightChars(_volume - other._volume, _weight - other._weight);
    }

    public VolumeWeightChars Add(VolumeWeightChars other)
    {
        return new VolumeWeightChars(_volume + other._volume, _weight + other._weight);
    }

    public VolumeWeightChars Multiply(uint count)
    {
        return new VolumeWeightChars(_volume * count, _weight * count);
    }

    public bool IsEmpty => _volume <= 0 || _weight <= 0;

    public int CompareTo(VolumeWeightChars other)
    {
        var volumeComparison = _volume.CompareTo(other._volume);
        if (volumeComparison != 0)
        {
            return volumeComparison;
        }

        return _weight.CompareTo(other._weight);
    }
}