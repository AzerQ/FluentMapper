using System;
using System.Collections.Generic;
using XmlMapper.Core.Models;

namespace XmlMapper.Core.Builders
{
    /// <summary>
    /// A builder class for creating a mapping configuration.
    /// </summary>
    public class MappingConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the MappingConfigurationBuilder class.
        /// </summary>
        public MappingConfigurationBuilder() { }

        private Dictionary<Type, ClassMap<object>> _classMaps = new Dictionary<Type, ClassMap<object>>();

        /// <summary>
        /// Adds a class configuration to the mapping configuration.
        /// </summary>
        /// <typeparam name="T">The type of the class to configure.</typeparam>
        /// <param name="objectXPath">The XPath expression for selecting objects of the specified type.</param>
        /// <param name="classMapProcess">An action to configure the class map for the specified type.</param>
        /// <returns>The current instance of the MappingConfigurationBuilder.</returns>
        public MappingConfigurationBuilder AddClassConfiguration<T>(string objectXPath, Action<ClassMap<T>> classMapProcess)
        {
            var classMap = new ClassMap<T>(typeof(T), objectXPath);
            classMapProcess(classMap);
            _classMaps[typeof(T)] = classMap.ToNoneGenericClassMap();
            return this;
        }

        /// <summary>
        /// Builds the mapping configuration using the added class configurations.
        /// </summary>
        /// <returns>The built mapping configuration.</returns>
        public MappingConfiguration Build()
        {
            return new MappingConfiguration(_classMaps);
        }
    }

}
