using System;
using System.Collections.Generic;
using XmlMapper.Core.Models;

namespace XmlMapper.Core.Builders
{
    public class MappingConfigurationBuilder
    {
        private Dictionary<Type, ClassMap<object>> _classMaps = new Dictionary<Type, ClassMap<object>>();

        public MappingConfigurationBuilder AddClassConfiguration<T>(string objectXPath, Action<ClassMap<T>> classMapProcess)
        {
            var classMap = new ClassMap<T>(typeof(T), objectXPath);
            classMapProcess(classMap);
            _classMaps[typeof(T)] = classMap.ToNoneGenericClassMap();
            return this;
        }

        public MappingConfiguration Build()
        {
            return new MappingConfiguration(_classMaps);
        }
    }

}
