using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Settings;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.UoW;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDb(this IServiceCollection services, Action<MongoSettings> settingsAction)
        {
            services.AddScoped<IMongoClient, MongoClient>();
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mongoSettings = new MongoSettings();
            settingsAction(mongoSettings);
            services.AddSingleton(mongoSettings);

            if (BsonClassMap.IsClassMapRegistered(typeof(DomainEntity)))
                return;

            BsonClassMap.RegisterClassMap<DomainEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.UnmapMember(x => x.ValidationResult);
            });
        }
    }
}