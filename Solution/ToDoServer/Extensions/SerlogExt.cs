using Serilog;
using System.Diagnostics;

namespace ToDoServer.Extensions;

public static class SeriLog
{
    
    private static void WriteColoredMessageToConsole(string message)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
    (hostingContext, loggerConfiguration) =>
    {
        try
        {
            loggerConfiguration.MinimumLevel.Override("System.Net.Http.HttpClient", Serilog.Events.LogEventLevel.Warning);
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        }
        catch (Exception exc)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(exc));
            throw;
        }
    };
}

