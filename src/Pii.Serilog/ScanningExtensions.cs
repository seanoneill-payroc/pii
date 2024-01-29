using System.Reflection;

namespace Pii.Serilog;

internal static class ScanningExtensions
{
    
    //TODO [perf]: Memoization
    internal static IEnumerable<Type> TypesWithPiiAttribute(this IEnumerable<Type> types) => types.Where(type => type.HasPropertyWithPiiAttribute());
    internal static bool HasPropertyWithPiiAttribute(this Type type) => type.GetPropertiesWithPii().Any();
    internal static IEnumerable<PropertyInfo> GetPropertiesWithPii(this Type type) => type.GetProperties().Where(x => x.GetCustomAttribute<PiiAttribute>() is not null);
}