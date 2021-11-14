using AdviceCore.Http;
using AdviceCore.Interfaces;
using AdviceInfrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdviceAPI.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAdviceService, AdviceService>();

            services.AddScoped<IHttpClientWrapper>(config =>
            {
                var httpclient = new HttpClient();
                httpclient.DefaultRequestHeaders.Add("Accept", "application/json");
                var wrapper = new HttpClientWrapper(httpclient);
                return wrapper;
            });

            return services;
        }
    }
}
