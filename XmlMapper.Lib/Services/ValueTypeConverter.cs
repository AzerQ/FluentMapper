using System;
using System.ComponentModel;

namespace XmlMapper.Core.Services
{
    /// <summary>
    /// Provides functionality for converting values to a specified destination type.
    /// Implements the <see cref="ITypeConverter"/> interface.
    /// </summary>
    public class ValueTypeConverter : ITypeConverter
    {
        public object ConvertToDestinationType(object source, Type destinationType)
        {
            if (source is null)
                return source;

            Type sourceType = source.GetType();

            if (sourceType == destinationType)
                return source;

            var converter = TypeDescriptor.GetConverter(destinationType);

            return converter.ConvertFrom(source);
        }
    }
}
