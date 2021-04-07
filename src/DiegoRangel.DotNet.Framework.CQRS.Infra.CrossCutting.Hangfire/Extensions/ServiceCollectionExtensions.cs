using System;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Hangfire.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatrSupportToHangfire(this IServiceCollection services)
        {
            services.AddScoped<IHangfireCommandsScheduler, HangfireCommandsScheduler>();
            services.AddScoped<IHangfireCommandsExecutor, HangfireCommandsExecutor>();
        }

        public static void AddHangfireWithMongoDb(this IServiceCollection services, Action<HangfireDatabaseSettings> settingsAction)
        {
            var databaseSettings = new HangfireDatabaseSettings();
            settingsAction(databaseSettings);

            services.AddHangfire(config =>
            {
                config.UseMongoStorage(
                    databaseSettings.ConnectionString,
                    new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            BackupStrategy = new NoneMongoBackupStrategy(),
                            MigrationStrategy = new MigrateMongoMigrationStrategy()
                        }
                    });
            });
        }

        public static void AddHangfireWithSqlServer(this IServiceCollection services, Action<HangfireDatabaseSettings> settingsAction)
        {
            var databaseSettings = new HangfireDatabaseSettings();
            settingsAction(databaseSettings);

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(databaseSettings.ConnectionString, new SqlServerStorageOptions
                    {
                        SchemaName = databaseSettings.Schema ?? "hangfire",
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true,
                    });
            });
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }

    public class HangfireDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string Schema { get; set; }
    }
}