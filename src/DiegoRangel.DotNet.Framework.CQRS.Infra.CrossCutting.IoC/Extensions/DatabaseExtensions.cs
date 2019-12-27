using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Settings;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Mappings;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddEfCore(this IServiceCollection services)
        {
            services.AddScoped<IChangeTrackerAuditer, ChangeTrackerAuditer>();
        }
        public static void AddMongoDb(this IServiceCollection services, Action<MongoSettings> action)
        {
            services.AddScoped<IMongoClient, MongoClient>();
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddScoped<IMongoMapper, MongoMapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mongoSettings = new MongoSettings();
            action(mongoSettings);
            services.AddSingleton(mongoSettings);
        }
    }
}