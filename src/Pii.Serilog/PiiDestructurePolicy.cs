using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace Pii.Serilog;

public class PiiDestructurePolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
    {
        if (!value.GetType().HasPropertyWithPiiAttribute())
        {
            result = propertyValueFactory.CreatePropertyValue(value);
            return false;
        }

        var props = value.GetType().GetTypeInfo().DeclaredProperties;
        var logEventProps = new List<LogEventProperty>();
        
        foreach(var prop in props)
        {
            var attr = prop.GetCustomAttribute<PiiAttribute>();
            if (attr is not null)
            {
                var strategy = attr.Strategy;
                var obj = prop.GetValue(value);
                //ToString feels hacky here, but should work for strings and primitives which would account for the vast majority of use cases
                var replacementstring = MaskingStrategies.Mask(strategy, obj?.ToString());
                logEventProps.Add(new(prop.Name, new ScalarValue(replacementstring)));
            }
            else
            {
                var logEventProp = propertyValueFactory.CreatePropertyValue(prop.GetValue(value));
                logEventProps.Add(new(prop.Name, logEventProp));
            }
        }
        result = new StructureValue(logEventProps);
        return true;
    }
}