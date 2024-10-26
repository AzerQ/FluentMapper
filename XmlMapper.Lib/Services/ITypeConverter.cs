using System;

namespace XmlMapper.Core.Services
{
    /// <summary>
    /// Defines a contract for converting objects to a specified destination type.
    /// </summary>
    public interface ITypeConverter
    {
        /// <summary>
        /// Converts the source object to the specified destination type.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="destinationType">The type to convert the source object to.</param>
        /// <returns>The converted object of the specified destination type.</returns>
        object ConvertToDestinationType(object source, Type destinationType);
    }
}
