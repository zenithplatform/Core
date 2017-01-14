using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Configuration
{
    public static class ConfigHelper
    {
        public static T ObjectFromConfig<T>(string configSectionName) where T : class
        {
            NameValueCollection nameValues = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(configSectionName);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            T instance = Activator.CreateInstance<T>();

            foreach(PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = null;

                if (nameValues.AllKeys.Contains(propertyName))
                {
                    if (property.PropertyType != typeof(System.String))
                        propertyValue = Convert.ChangeType(nameValues[propertyName], property.PropertyType);
                    else
                        propertyValue = nameValues[propertyName];

                    property.SetValue(instance, propertyValue);
                }
            }

            return instance;
        }
    }
}
