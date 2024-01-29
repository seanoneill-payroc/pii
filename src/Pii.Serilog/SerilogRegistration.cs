using Serilog;
using System.Reflection;

namespace Pii.Serilog;

public static class SerilogRegistration
{
    public static LoggerConfiguration AddPiiDestructurer(this LoggerConfiguration loggerConfiguration, Assembly assembly)
    {
        return loggerConfiguration.Destructure.With<PiiDestructurePolicy>();
    }
}