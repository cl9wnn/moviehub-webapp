using Serilog;
using Serilog.Events;

namespace API.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var seqUrl = builder.Configuration["Seq:Url"];

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Seq(seqUrl!)
            .Enrich.WithProperty("Application", "WebApp")
            .CreateLogger();
        
        builder.Host.UseSerilog();

        return builder;
    }
}