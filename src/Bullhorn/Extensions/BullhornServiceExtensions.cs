using CodeCapital.Bullhorn.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Polly;
using System;
using System.Net.Http;


namespace CodeCapital.Bullhorn.Extensions
{
    public static class BullhornServiceExtensions
    {
        /// <summary>
        /// Adds Bullhorn REST API
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        public static void AddBullhorn(this IServiceCollection services, IConfiguration configuration, string key = "BullhornSettings")
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddBullhorn(configuration.GetSection(key).Get<BullhornSettings>());
        }

        public static void AddBullhorn(this IServiceCollection services, BullhornSettings configureSettings)
        {
            services.TryAddSingleton(Options.Create(configureSettings));
            services.AddHttpClient<BullhornApi>()
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(20))
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3) }));
        }
    }
}

