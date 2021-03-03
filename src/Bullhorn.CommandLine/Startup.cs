using Bullhorn.CommandLine.Services;
using CodeCapital.Bullhorn.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;

namespace Bullhorn.CommandLine
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{GetEnvironmentVariable()}.json", true, true)
                .AddEnvironmentVariables();

            if (IsDevelopment()) builder.AddUserSecrets(typeof(Program).Assembly);

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                // Change this to Information to see SQL Logs
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.WriteTo.Console(outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                //.WriteTo.File(path: "logs/log.log-", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14)
                .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            static bool IsDevelopment()
            {
                var devEnvironmentVariable = GetEnvironmentVariable();

                return string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
            }

            static string? GetEnvironmentVariable() => Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddLogging(builder => builder
                //.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
                .AddSerilog(dispose: true)
            );

            services.AddBullhorn(Configuration, "BullhornSettings:RestApi");

            services.AddScoped<PlayGroundService>();
        }
    }
}