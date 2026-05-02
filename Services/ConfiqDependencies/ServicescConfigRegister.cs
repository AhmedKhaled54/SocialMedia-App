using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

using Services.FilesServices;
using Services.RealTimeServices.MessageServices;
using Services.RealTimeServices.NotificationsServices;
using Services.Services.AuthanticationServices;
using Services.Services.CachServices;
using Services.Services.EmailServices;
using Services.Services.FollowService;
using Services.Services.OtpService;
using StackExchange.Redis;
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
            services.AddTransient<IFileServices,FileServices>();
            services.AddTransient<ICachServices,CachServices>();
            services.AddTransient<IOtpServices, OtpServices>();
            services.AddTransient<INotificationServices,NotificationServices>();
            services.AddTransient<IPrivateMessageServices,PrivateMessageServices>();
            services.AddTransient<IFollowServices,FollowServices>();


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
