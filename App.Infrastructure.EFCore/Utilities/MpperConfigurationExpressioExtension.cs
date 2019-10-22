using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Infrastructure.Utilities
{
    public static class MpperConfigurationExpressioExtension
    {
        public static void ApplyConfiguration(this IMapperConfigurationExpression configuration)
        {
            configuration.ApplyConfiguration(Assembly.GetEntryAssembly());
        }

        public static void ApplyConfiguration(this IMapperConfigurationExpression configuration, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var profiles = assembly.GetTypes().Distinct().Where(type => type.IsClass && !type.IsAbstract &&
                type.BaseType == typeof(Profile))
                .Select(type => (Profile)Activator.CreateInstance(type));

                foreach (var profile in profiles)
                {
                    configuration.AddProfile(profile);
                }
            }
        }
    }
}
