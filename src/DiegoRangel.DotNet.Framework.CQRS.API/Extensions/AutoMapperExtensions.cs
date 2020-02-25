using System;
using System.Reflection;
using AutoMapper;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapperWithSettings(this IServiceCollection services, Action<AutoMapperSettings> autoMapperSettingsBuilder, params Assembly[] assemblies)
        {
            var autoMapperSettings = new AutoMapperSettings();
            autoMapperSettingsBuilder(autoMapperSettings);
            services.AddSingleton(new AutoMapperAssemblies(assemblies));

            services.AddAutoMapper(opt =>
            {
                if (autoMapperSettings.UseStringTrimmingTransformers)
                    opt.ValueTransformers.Add<string>(value => value != null ? value.Trim() : null);
            }, assemblies);
        }

        public class AutoMapperSettings
        {
            public bool UseStringTrimmingTransformers { get; set; }
        }
    }
}