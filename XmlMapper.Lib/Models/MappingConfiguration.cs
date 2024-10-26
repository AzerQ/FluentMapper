using System;
using System.Collections.Generic;

namespace XmlMapper.Core.Models
{
    public class MappingConfiguration
    {
        private readonly Dictionary<Type, ClassMap<object>> _classMaps;

        public MappingConfiguration(Dictionary<Type, ClassMap<object>> classMaps)
        {
            _classMaps = classMaps;
        }

        public ClassMap<object> GetClassMap(Type type) => _classMaps.TryGetValue(type, out var classMap) ? classMap : null;
    }

}
