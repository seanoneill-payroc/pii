namespace Pii;

[AttributeUsage(AttributeTargets.Property)]
public class PiiAttribute:Attribute
{
    public MaskingStrategy Strategy { get; }

    public PiiAttribute(MaskingStrategy strategy)
    {
        Strategy = strategy;
    }
}