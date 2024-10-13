using System;
using System.ComponentModel;

namespace XmlMapper
{
    public class ValueConverter : IConverter
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
