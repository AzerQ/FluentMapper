using System.Collections.Generic;
using XmlMapper.Core.Models;

namespace XmlMapper.Core
{
    /// <summary>
    /// Provides functionality for mapping XML data to objects using a specified mapping configuration.
    /// </summary>
    public interface IXmlMapper
    {
        /// <summary>
        /// Maps a collection of XML elements to a list of objects of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to map to.</typeparam>
        /// <param name="config">The mapping configuration that defines how to map XML elements to object properties.</param>
        /// <param name="xmlString">The XML string to map.</param>
        /// <returns>A list of objects of the specified type, mapped from the XML elements.</returns>
        List<T> MapToCollection<T>(MappingConfiguration config, string xmlString);

        /// <summary>
        /// Maps the XML string to a single object of type T using the provided mapping configuration.
        /// </summary>
        /// <typeparam name="T">The type of the object to map to.</typeparam>
        /// <param name="config">The mapping configuration that defines how to map XML elements to object properties.</param>
        /// <param name="xmlString">The XML string to map.</param>
        /// <returns>The mapped object of type T.</returns>
        T MapToObject<T>(MappingConfiguration config, string xmlString);
    }
}