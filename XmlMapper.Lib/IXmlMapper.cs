using System.Collections.Generic;
using XmlMapper.Core.Models;

namespace XmlMapper.Core
{
    public interface IXmlMapper
    {
        List<T> MapToCollection<T>(MappingConfiguration config, string xmlString);
        T MapToObject<T>(MappingConfiguration config, string xmlString);
    }
}