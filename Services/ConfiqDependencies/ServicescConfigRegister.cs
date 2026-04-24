using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Services.Services.AuthanticationServices;
using Services.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConfiqDependencies
{
    public static  class ServicescConfigRegister
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthanticationServices, AuthanticationServices>();
            services.AddTransient<IEmailServices, EmailServices>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(c =>
            {
                var actioncontext = c.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = c.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actioncontext);

            });

            return services;
        }
    }
}
