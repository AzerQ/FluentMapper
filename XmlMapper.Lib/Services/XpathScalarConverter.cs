using System;
using System.Collections;
using System.Xml.Linq;

namespace XmlMapper.Core.Services
{
    /// <summary>
    /// Represents an exception that is thrown when there is an issue converting a value in XPath.
    /// </summary>
    public class XpathValueConvertException : Exception
    {
        public XpathValueConvertException()
        {
        }

        public XpathValueConvertException(string message) : base(message)
        {
        }

        public XpathValueConvertException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Provides functionality for converting XPath results to a scalar value.
    /// Implements the <see cref="IXpathScalarConverter"/> interface.
    /// </summary>
    public class XpathScalarConverter : IXpathScalarConverter
    {
        /// <summary>
        /// Retrieves the value from the provided XPath result object.
        /// If the result is a collection, it extracts the first element.
        /// Supports extracting values from XElement, XAttribute, string, double, and bool types.
        /// Throws an exception if the type is not supported.
        /// </summary>
        /// <exception cref="XpathValueConvertException"></exception>
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
                    throw new XpathValueConvertException(
                        $"Convert xpath result of type ({scalarValue?.GetType()}) not supported!");
            }
        }
    }
}