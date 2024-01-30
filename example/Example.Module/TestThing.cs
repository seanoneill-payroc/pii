using Pii;

namespace Example.Module;

public record TestThing(
    [property: Pii(MaskingStrategy.FullMask)] 
    string Value, 
    [property:Pii(MaskingStrategy.LastFour)]
    string CreditCard)
{
}

public record Complex(TestThing Child, string OtherValue);