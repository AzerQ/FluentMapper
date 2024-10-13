using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlMapper
{
    public static class XmlMapper
    {

        private readonly static IConverter _converter;

        static XmlMapper()
        {
            _converter = new ValueConverter();
        }
        private static IList MapToCollection(Type itemType, MappingConfiguration config, string xmlString)
        {
            var list = new List<object>();
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            var classMap = config.GetClassMap(itemType)
                ?? throw new Exception($"No mapping configuration found for type {itemType}");

            var selectedNodes = doc.SelectNodes(classMap.GetObjectXPath())
                ?? throw new Exception("Elements not found for the specified XPath");



            foreach (XmlNode node in selectedNodes)
            {
                var obj = Activator.CreateInstance(itemType);

                foreach (var propMap in classMap.GetPropertyMaps())
                {
                    var valueNode = node.SelectSingleNode(propMap.XPath);
                    if (valueNode != null)
                    {
                        var value = valueNode.InnerText;
                        var propInfo = propMap.Property;
                        var convertedValue = _converter.ConvertToDestinationType(value, propInfo.PropertyType);

                        if (propMap.Converter != null)
                            convertedValue = propMap.Converter.DynamicInvoke(convertedValue);

                        propInfo.SetValue(obj, convertedValue);
                    }
                }

                foreach (var linkedPropMap in classMap.GetLinkedPropertyMaps())
                {

                    var linkedObjectsList = MapToCollection(linkedPropMap.ItemType, config, xmlString);

                    if (linkedPropMap.IsCollection)
                        linkedPropMap.Property.SetValue(obj, linkedObjectsList.CastToTyped(linkedPropMap.ItemType));

                    else
                        linkedPropMap.Property.SetValue(obj, linkedObjectsList[0]);

                }

                list.Add(obj);
            }

            return list;

        }

        public static List<T> MapToCollection<T>(MappingConfiguration config, string xmlString)
        {
          return MapToCollection(typeof(T), config, xmlString)
                .Cast<T>()
                .ToList();
        }

        public static T MapToObject<T>(MappingConfiguration config, string xmlString)
        {
            return MapToCollection<T>(config, xmlString).First();
        }
    }

}
