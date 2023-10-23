using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ShortCleanLinqExtensions.src.Utils.AutoRegisterDI;
using ShortCleanLinqExtensions.src.Utils.Interfaces;
using System.Reflection;

namespace ShortCleanLinqExtensions.src.Extensions
{
    public static class DIScannerExtension
    {
        /// <summary>
        /// When add extension method you can automatic read all you interface extend interfaces (IScopedService, ISingletonService ou ISingletonService) and apply register native
        /// )
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDIScannerIOC(this IServiceCollection services)
        {
            Type scopedServiceType = typeof(IScopedService);
            Type transientServiceType = typeof(ITransientService);
            Type singletonServiceType = typeof(ISingletonService);

            var scopedServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(scopedServiceType.IsAssignableFrom)
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null);


            var transientServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(transientServiceType.IsAssignableFrom)
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null);


            var singletonServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(singletonServiceType.IsAssignableFrom)
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null);

            foreach (var scopedService in scopedServices)
            {
                if (scopedServiceType.IsAssignableFrom(scopedService.Service))
                {
                    _ = services.AddScoped(scopedService.Service, scopedService.Implementation);
                }
            }

            foreach (var transientService in transientServices)
            {
                if (transientServiceType.IsAssignableFrom(transientService.Service))
                {
                    _ = services.AddScoped(transientService.Service, transientService.Implementation);
                }
            }

            foreach (var singletonService in singletonServices)
            {
                if (singletonServiceType.IsAssignableFrom(singletonService.Service))
                {
                    _ = services.AddScoped(singletonService.Service, singletonService.Implementation);
                }
            }

            return services;
        }

        /// <summary>
        /// When add extension method you can automatic read all you interface extend interfaces (IScopedService, ISingletonService ou ISingletonService) and apply register using nuget scrutor
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDIScannerIOCWithScrutor(this IServiceCollection services)
        {
            services.Scan(delegate (ITypeSourceSelector scan)
            {
                scan.FromApplicationDependencies().AddClasses(delegate (IImplementationTypeFilter classes)
                {
                    classes.AssignableTo<ITransientService>();
                }).UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(delegate (IImplementationTypeFilter classes)
                    {
                        classes.AssignableTo<IScopedService>();
                    })
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(delegate (IImplementationTypeFilter classes)
                    {
                        classes.AssignableTo<ISingletonService>();
                    })
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
            });

            services.AddValidatorsFromAssemblies(from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                                 where !assembly.IsDynamic
                                                 select assembly);

            return services;
        }
    }
}