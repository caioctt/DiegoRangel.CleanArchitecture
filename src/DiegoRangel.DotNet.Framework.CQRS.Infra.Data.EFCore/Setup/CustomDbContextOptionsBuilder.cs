using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Setup
{
    public static class CustomDbContextOptionsBuilder
    {
        public static DbContextOptionsBuilder<TDbContext> BuildOptions<TDbContext>(string connectionStringName) where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionStringName));

            return optionsBuilder;
        }
    }
}