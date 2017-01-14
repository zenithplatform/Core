using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Shared.Reflection;

namespace Zenith.Core.DataProviders.Metadata
{
    public static class ObjectMetadataReader
    {
        public static string GetDbName<T>(T entity) where T : class
        {
            DbNameAttribute attr = ReflectionHelper.GetAttribute<DbNameAttribute>(entity);
            return (attr != null) ? attr.DbName : "";
        }

        public static string GetTableName<T>(T entity) where T : class
        {
            TableNameAttribute attr = ReflectionHelper.GetAttribute<TableNameAttribute>(entity);
            return (attr != null) ? attr.TableName : "";
        }

        public static string GetUniqueIdentifier<T>(T entity) where T : class
        {
            UniqueIdAttribute attr = ReflectionHelper.GetAttribute<UniqueIdAttribute>(entity);
            return (attr != null) ? attr.UniqueIdName : "";
        }
    }
}
