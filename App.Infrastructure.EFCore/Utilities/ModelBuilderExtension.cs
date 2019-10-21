using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pluralize.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Infrastructure.Utilities
{
    public static class ModelBuilderExtension
    {
        public static void AutoAddDbSetClass<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            new ModelBuilderDecoraor(modelBuilder).AddDbSetClass<BaseType>(assemblies);
        }

        public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            new ModelBuilderDecoraor(modelBuilder);
        }

        public static void SetUpSequentialGuid(this ModelBuilder modelBuilder)
        {
            new ModelBuilderDecoraor(modelBuilder).SetUpSequentialGuidToId();
        }
    }

    public class ModelBuilderDecoraor
    {
        private readonly ModelBuilder _modelBuilder;

        public ModelBuilderDecoraor(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void AddDbSetClass<BaseType>(params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
               .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                _modelBuilder.Entity(type);
        }

        public void AddPluralizingTableName()
        {
            Pluralizer pluralizer = new Pluralizer();
            foreach (IMutableEntityType entityType in _modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.Relational().TableName;
                entityType.Relational().TableName = pluralizer.Pluralize(tableName);
            }
        }

        public void SetUpSequentialGuidToId()
        {
            var property = _modelBuilder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetProperties())
                            .Where(p => p.ClrType == typeof(Guid) && p.IsKey());

            foreach (var p in property)
            {
                p.Relational().DefaultValueSql = "newsequentialid()";
            }
        }
    }
}
