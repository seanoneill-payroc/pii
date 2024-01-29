namespace Pii;

public enum MaskingStrategy
{
    FullMask
}

public static class MaskingStrategies
{

    private static Dictionary<MaskingStrategy, Func<object, string>> _mapping = new()
    {
        { MaskingStrategy.FullMask, (obj) => new('*', (obj?.ToString() ?? "").Length) }, 
    };

    public static string Mask(MaskingStrategy strategy, object input) => _mapping[strategy]?.Invoke(input) ?? throw new NotSupportedException();
}