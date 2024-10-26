using XmlMapper.Core.Services;

namespace XmlMapper.Core
{
    /// <summary>
    /// Provides a factory for creating instances of <see cref="IXmlMapper"/>.
    /// </summary>
    public static class XmlMapperFactory
    {

        private static IXmlMapper _defaultXmlMapper;

        private static ITypeConverter GetValueConverter() => new ValueTypeConverter();

        private static IXpathScalarConverter GetXpathScalarConverter() => new XpathScalarConverter();

        /// <summary>
        /// Gets the default instance of <see cref="IXmlMapper"/>.
        /// If the default instance is not yet created, it will be created using <see cref="GetValueConverter"/> and <see cref="GetXpathScalarConverter"/>.
        /// </summary>
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
