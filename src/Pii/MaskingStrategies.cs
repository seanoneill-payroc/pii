namespace Pii;

public enum MaskingStrategy
{
    FullMask,
    LastFour,
}

public static class MaskingStrategies
{

    private static Dictionary<MaskingStrategy, Func<object, string>> _mapping = new()
    {
        { MaskingStrategy.FullMask, (obj) => new('*', (obj?.ToString() ?? "").Length) },
        { MaskingStrategy.LastFour, (obj) => new string('*', obj.ToString()[..^4].Length) + obj?.ToString()[^4..] }
    };

    public static string Mask(MaskingStrategy strategy, object input) => _mapping[strategy]?.Invoke(input) ?? throw new NotSupportedException();
}