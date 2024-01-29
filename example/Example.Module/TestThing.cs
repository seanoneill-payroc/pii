using Pii;

namespace Example.Module;

public record TestThing(string Value)
{
    [Pii(MaskingStrategy.FullMask)]
    public string Value { get; init; } = Value;
}