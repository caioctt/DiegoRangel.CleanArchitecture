﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyVarcharConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var entity in entityType.GetProperties())
                {
                    if (entity.ClrType == typeof(string) || entity.DeclaringEntityType.ClrType == typeof(string))
                        entity.IsUnicode(false);
                }
            }
        }

        public static void ApplyDateTimeConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var entity in entityType.GetProperties())
                {
                    if (entity.ClrType == typeof(DateTime) || entity.DeclaringEntityType.ClrType == typeof(DateTime) || entity.ClrType == typeof(DateTime?) || entity.DeclaringEntityType.ClrType == typeof(DateTime?))
                    {
                        entity.Relational().ColumnType = "datetime2";
                    }
                }
            }
        }

        public static void ApplyMappings(this ModelBuilder modelBuilder)
        {
            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault(m => m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1");

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = applyGenericMethod?.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreteMethod?.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }

        public static void DisableCascadeDelete(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}