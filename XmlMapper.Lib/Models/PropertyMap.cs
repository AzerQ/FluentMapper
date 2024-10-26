using System;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    /// <summary>
    /// Represents a mapping between a property and an XPath expression.
    /// </summary>
    public class PropertyMap
    {
        /// <summary>
        /// Gets the property being mapped.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Gets the XPath expression for selecting the value to map to the property.
        /// </summary>
        public string XPath { get; }

        /// <summary>
        /// Gets the post-conversion delegate to apply after mapping the value.
        /// </summary>
        public Delegate PostConverter { get; }

        /// <summary>
        /// Gets the pre-conversion delegate to apply before mapping the value.
        /// </summary>
        public Delegate PreConverter { get; }

        /// <summary>
        /// Initializes a new instance of the PropertyMap class.
        /// </summary>
        /// <param name="property">The property being mapped.</param>
        /// <param name="xpath">The XPath expression for selecting the value to map to the property.</param>
        /// <param name="postConverter">The post-conversion delegate to apply after mapping the value (optional).</param>
        /// <param name="preConverter">The pre-conversion delegate to apply before mapping the value (optional).</param>
        public PropertyMap(PropertyInfo property, string xpath, Delegate postConverter = null, Delegate preConverter = null)
        {
            Property = property;
            XPath = xpath;
            PostConverter = postConverter;
            PreConverter = preConverter;
        }
    }

}
