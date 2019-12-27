using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Mappings;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.IoC.Extensions
{
    public class DatabaseExtensions
    {
        public static void AddEfCore(IServiceCollection services)
        {
            services.AddScoped<IChangeTrackerAuditer, ChangeTrackerAuditer>();
        }
        public static void AddMongoDb(IServiceCollection services)
        {
            services.AddScoped<IMongoClient, MongoClient>();
            services.AddScoped<IMongoContext, MongoDbContext>();
            services.AddScoped<IMongoMapper, MongoMapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}