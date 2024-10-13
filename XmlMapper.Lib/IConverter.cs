using System;

namespace XmlMapper
{
    public interface IConverter
    {
        object ConvertToDestinationType(object source, Type destinationType);
    }
}
