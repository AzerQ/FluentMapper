using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    public class LinkedPropertyMap
    {
        public PropertyInfo Property { get; private set; }

        public bool IsCollection { get; private set; }

        public Type ItemType { get; private set; }

        public LinkedPropertyMap(PropertyInfo property)
        {
            Property = property;

            Type propType = Property.PropertyType;

            IsCollection = propType.IsGenericType &&
                propType.GetGenericTypeDefinition() == typeof(List<>);

            ItemType = IsCollection ? propType.GetGenericArguments().First() : propType;

        }
    }

}
