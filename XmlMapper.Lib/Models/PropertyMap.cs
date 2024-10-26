using System;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    public class PropertyMap
    {
        public PropertyInfo Property { get; }
        public string XPath { get; }
        public Delegate PostConverter { get; }
        public Delegate PreConverter { get; }

        public PropertyMap(PropertyInfo property, string xpath, Delegate postConverter = null, Delegate preConverter = null)
        {
            Property = property;
            XPath = xpath;
            PostConverter = postConverter;
            PreConverter = preConverter;
        }
    }

}
