using System;

namespace XmlMapper.Core.Services
{
    public interface ITypeConverter
    {
        object ConvertToDestinationType(object source, Type destinationType);
    }
}
