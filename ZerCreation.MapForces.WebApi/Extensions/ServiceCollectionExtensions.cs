using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using ZerCreation.MapForces.WebApi.Configuration;
using ZerCreation.MapForcesEngine.Configuration;

namespace ZerCreation.MapForces.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer ApplicationContainer { get; private set; }

        public static IServiceProvider ConfigureDependencyInjection(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WebApiModule>();
            builder.RegisterModule<EngineModule>();
            builder.Populate(services);

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            });

        }
    }
}
