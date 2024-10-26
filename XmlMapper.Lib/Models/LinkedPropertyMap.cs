using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    /// <summary>
    /// Represents a mapping between a property and a linked property in an object graph.
    /// </summary>
    public class LinkedPropertyMap
    {
        /// <summary>
        /// Gets the property being mapped.
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the property is a collection.
        /// </summary>
        public bool IsCollection { get; private set; }

        /// <summary>
        /// Gets the type of items in the collection, or the type of the property if it is not a collection.
        /// </summary>
        public Type ItemType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the LinkedPropertyMap class.
        /// </summary>
        /// <param name="property">The property being mapped.</param>
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
