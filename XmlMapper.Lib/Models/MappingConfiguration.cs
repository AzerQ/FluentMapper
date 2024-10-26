using System;
using System.Collections.Generic;

namespace XmlMapper.Core.Models
{
    /// <summary>
    /// Represents a configuration for mapping objects to XML.
    /// </summary>
    public class MappingConfiguration
    {
        private readonly Dictionary<Type, ClassMap<object>> _classMaps;

        /// <summary>
        /// Initializes a new instance of the MappingConfiguration class.
        /// </summary>
        /// <param name="classMaps">The class maps for mapping objects to XML.</param>
        public MappingConfiguration(Dictionary<Type, ClassMap<object>> classMaps)
        {
            _classMaps = classMaps;
        }

        /// <summary>
        /// Retrieves the class map for the specified type.
        /// </summary>
        /// <param name="type">The type to retrieve the class map for.</param>
        /// <returns>The class map for the specified type, or null if no class map exists for the type.</returns>
        public ClassMap<object> GetClassMap(Type type) => _classMaps.TryGetValue(type, out var classMap) ? classMap : null;
    }

}
