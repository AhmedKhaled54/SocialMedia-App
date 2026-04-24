using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.ConfiqurationDependancies
{
    public static  class CoreConfiqRegister
    {
        public static IServiceCollection AddCoreConfiqRegister(this IServiceCollection services)
        {
            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
