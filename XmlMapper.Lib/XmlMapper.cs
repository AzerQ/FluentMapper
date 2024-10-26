using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using XmlMapper.Core.Extensions;
using XmlMapper.Core.Models;
using XmlMapper.Core.Services;

namespace XmlMapper.Core
{
    public class XmlMapper : IXmlMapper
    {

        private readonly ITypeConverter _valueTypeConverter;
        private readonly IXpathScalarConverter _xpathScalarConverter;

        public XmlMapper(ITypeConverter typeConverter, IXpathScalarConverter xpathScalarConverter)
        {
            _valueTypeConverter = typeConverter;
            _xpathScalarConverter = xpathScalarConverter;
        }

        private object ProcessXpathResult(object xpathResult, PropertyMap propertyMap)
        {
            string xpathResultStr = _xpathScalarConverter.GetXpathResultValue(xpathResult);

            object result = (propertyMap.PreConverter != null) ?
                propertyMap.PreConverter.DynamicInvoke(xpathResultStr) :
                _valueTypeConverter.ConvertToDestinationType(xpathResultStr, propertyMap.Property.PropertyType);

            return (propertyMap.PostConverter == null) ? result :
                    propertyMap.PostConverter.DynamicInvoke(result);

        }

        private object MapToItem(Type itemType, MappingConfiguration config, XElement xElement, string fullXmlContext)
        {
            var classMap = config.GetClassMap(itemType)
                ?? throw new Exception($"No mapping configuration found for type {itemType}");

            var obj = Activator.CreateInstance(itemType);

            foreach (var propMap in classMap.GetPropertyMaps())
            {
                var xpathResult = xElement.XPathEvaluate(propMap.XPath);

                var convertedValue = ProcessXpathResult(xpathResult, propMap);

                propMap.Property.SetValue(obj, convertedValue);
            }

            foreach (var linkedPropMap in classMap.GetLinkedPropertyMaps())
            {

                var linkedObjectsList = MapToCollection(linkedPropMap.ItemType, config, fullXmlContext);

                if (linkedPropMap.IsCollection)
                    linkedPropMap.Property.SetValue(obj, linkedObjectsList.CastToTyped(linkedPropMap.ItemType));

                else
                    linkedPropMap.Property.SetValue(obj, linkedObjectsList[0]);

            }

            return obj;
        }


        private IList MapToCollection(Type itemType, MappingConfiguration config, string xmlString)
        {
            var list = new List<object>();
            var xdoc = XDocument.Load(new StringReader(xmlString));

            var classMap = config.GetClassMap(itemType);

            var selectedNodes = xdoc.XPathSelectElements(classMap.GetObjectXPath())
                ?? throw new Exception("Elements not found for the specified XPath");

            return selectedNodes.Select(node => MapToItem(itemType, config, node, xmlString)).ToList();

        }

        public List<T> MapToCollection<T>(MappingConfiguration config, string xmlString)
        {
            return MapToCollection(typeof(T), config, xmlString)
                  .Cast<T>()
                  .ToList();
        }

        public T MapToObject<T>(MappingConfiguration config, string xmlString)
        {
            return MapToCollection<T>(config, xmlString).First();
        }
    }

}
