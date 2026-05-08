using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Wrapper.Hangfire;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConfiqDependencies
{
    public static class ServicesDependanciesConfiq
    {
        public static IServiceCollection AddServicesDependanciesConfiq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(r =>
            {
                var connect = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(connect);

            });


            return services;
        }
    }
}
