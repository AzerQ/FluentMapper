using XmlMapper.Core.Services;

namespace XmlMapper.Core
{
    public static class XmlMapperFactory
    {
        private static IXmlMapper _defaultXmlMapper;

        private static ITypeConverter GetValueConverter() => new ValueTypeConverter();

        private static IXpathScalarConverter GetXpathScalarConverter() => new XpathScalarConverter();

        public static IXmlMapper DefaultXmlMapper
        {
            get
            {
                if (_defaultXmlMapper is null)
                    _defaultXmlMapper = new XmlMapper(GetValueConverter(), GetXpathScalarConverter());

                return _defaultXmlMapper;
            }
        }

    }

}
