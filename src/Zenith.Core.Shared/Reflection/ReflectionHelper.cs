using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Reflection
{
    public static class ReflectionHelper
    {
        public static T GetAttribute<T>(object instance)
        {
            IEnumerable<object> attributes = instance.GetType().GetCustomAttributes(typeof(T), false);

            if(attributes != null && attributes.Count() > 0)
            {
                object singleAttribute = attributes.First();

                if (singleAttribute != null)
                    return (T)singleAttribute;
                else
                    return default(T);
            }

            return default(T);
        }
    }
}
