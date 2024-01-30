# Pii

The idea here is that the Pii project exists to isolate the Pii attribute, and transformation logic. It is important to note that this library has no dependencies.

## PiiAttribute(Property Attribute)

Decorator to signal this property contains sensitive information and a flag to identify which masking strategy to use.

__Usage:__
 ```csharp 
 [Pii(MaskingStrategy.FullMask]
 public string Sensitive { get; init;}
 ```

Maintenance, is straight forward, if you need additional strategies, simply add to the enum, and provide a Func<object, string> to the dictionary.

# Pii.Serilog

an implementation of a Serilog destructurer that leverages this flag, host systems would take dependency on this, to augment their serilog configuration to perform the masking on the way to logging system.

Note: 
this is a low effort implementation, using reflection, which is not ideal, but somewhat justified by the fact that the logging is buffered, and should impact perf of consumers. There are however opportunities to improve performance with memoization in the existing code, or sourcegen potentially (potential concerns about dependency leaking here)

# Examples

The example shows how this would be implemented in a module/host environment similar to the one we use now, where the module has no knowledge of the log system, and the host system has no knowledge of the sensitive information in the module.

The developer experience her is that the devs would get an mvf that includes sensitive information, they simply need to decorate the property and how that information is handled downstream is reduced to a sanity check.