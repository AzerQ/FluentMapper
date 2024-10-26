using System;
using System.Collections;
using System.Xml.Linq;

namespace XmlMapper.Core.Services
{
    public class XpathScalarConverter : IXpathScalarConverter
    {
        public string GetXpathResultValue(object xpathResult)
        {
            object scalarValue = xpathResult;

            if (xpathResult is IEnumerable collection)
            {
                var enumerator = collection.GetEnumerator();
                enumerator.MoveNext();
                scalarValue = enumerator.Current;
            }

            switch (scalarValue)
            {
                case XElement xElement:
                    return xElement.Value;

                case XAttribute xAttribute:
                    return xAttribute.Value;

                case string str:
                    return str;

                case double d:                 
                case bool b:
                    return scalarValue.ToString();

                default:
                    throw new ArgumentException
                        ($"Convert xpath result of type ({scalarValue?.GetType()}) not supported!", nameof(scalarValue));
            }
        }
    }
}
