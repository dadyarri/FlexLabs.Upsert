#if !NETCORE3
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlexLabs.EntityFrameworkCore.Upsert.Runners
{
    internal static class ProxyExtensions
    {
        public static string GetSchema(this IEntityType entity)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return entity.Relational().Schema;

            var method = typeof(RelationalDatabaseFacadeExtensions).Assembly.GetType("Microsoft.EntityFrameworkCore.RelationalEntityTypeExtensions")
                .GetMethod("GetSchema", BindingFlags.Static);
            return (string)method.Invoke(null, new object[] { entity });
        }

        public static string GetTableName(this IEntityType entity)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return entity.Relational().TableName;

            var method = typeof(RelationalDatabaseFacadeExtensions).Assembly.GetType("Microsoft.EntityFrameworkCore.RelationalEntityTypeExtensions")
                .GetMethod("GetTableName", BindingFlags.Static);
            return (string)method.Invoke(null, new object[] { entity });
        }

        public static PropertySaveBehavior GetAfterSaveBehavior(this IProperty property)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return property.AfterSaveBehavior;

            var method = typeof(IProperty).Assembly.GetType("Microsoft.EntityFrameworkCore.PropertyExtensions")
                .GetMethod("GetAfterSaveBehavior", BindingFlags.Static);
            return (PropertySaveBehavior)method.Invoke(null, new object[] { property });
        }

        public static string GetColumnName(this IProperty property)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return property.Relational().ColumnName;

            var method = typeof(RelationalDatabaseFacadeExtensions).Assembly.GetType("Microsoft.EntityFrameworkCore.RelationalPropertyExtensions")
                .GetMethod("GetColumnName", BindingFlags.Static);
            return (string)method.Invoke(null, new object[] { property });
        }

        public static object GetDefaultValue(this IProperty property)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return property.Relational().DefaultValue;

            var method = typeof(RelationalDatabaseFacadeExtensions).Assembly.GetType("Microsoft.EntityFrameworkCore.RelationalPropertyExtensions")
                .GetMethod("GetDefaultValue", BindingFlags.Static);
            return method.Invoke(null, new object[] { property });
        }

        public static string GetDefaultValueSql(this IProperty property)
        {
            if (typeof(IProperty).GetProperty("AfterSaveBehavior") != null)
                return property.Relational().DefaultValueSql;

            var method = typeof(RelationalDatabaseFacadeExtensions).Assembly.GetType("Microsoft.EntityFrameworkCore.RelationalPropertyExtensions")
                .GetMethod("GetDefaultValueSql", BindingFlags.Static);
            return (string)method.Invoke(null, new object[] { property });
        }
    }
}
#endif
