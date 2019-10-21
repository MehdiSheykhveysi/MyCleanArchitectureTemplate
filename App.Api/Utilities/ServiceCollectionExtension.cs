using App.ApplicationService.Shaared.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Reflection;

namespace App.Api.Utilities
{
    public static class ServiceCollectionExtension
    {
        public static void AddIdentityService<TUser, TKey, TContext>(this IServiceCollection services) where TUser : IdentityUser<TKey> where TContext : DbContext where TKey : IEquatable<TKey>
        {
            new ServiceCollectionDecorator(services).AddIdentityService<TUser, TKey, TContext>();
        }

        public static void AddScopedDependencies(this IServiceCollection services, Assembly targetAssembly)
        {
            new ServiceCollectionDecorator(services).AddScopedDependencies(targetAssembly);
        }

        public static void AddControllers(this IServiceCollection services)
        {
            new ServiceCollectionDecorator(services).AddControlLers();
        }
    }

    public class ServiceCollectionDecorator
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionDecorator(IServiceCollection services)
        {
            _services = services;
        }

        public void AddIdentityService<TUser, TKey, TContext>() where TUser : IdentityUser<TKey> where TContext : DbContext where TKey : IEquatable<TKey>
        {
            _services.AddIdentityCore<TUser>(setupAction =>
            {
            }).AddEntityFrameworkStores<TContext>();
        }

        public void AddScopedDependencies(Assembly targetAssembly)
        {
            _services.Scan(scan => scan
              .FromAssemblies(targetAssembly)
                .AddClasses(classes => classes.WithAttribute(typeof(ServiceMark)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .AsSelf()
                            .WithScopedLifetime());
        }

        public void AddControlLers()
        {
            _services.AddMvcCore().AddJsonFormatters().AddApiExplorer().AddFormatterMappings().AddDataAnnotations().AddJsonFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddCookieTempDataProvider();
        }
    }
}
