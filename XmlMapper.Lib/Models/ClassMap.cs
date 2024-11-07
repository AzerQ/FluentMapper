using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    /// <summary>
    /// Represents a mapping between a source object and an XML document.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    public class ClassMap<TSource>
    {
        private readonly Type _type;
        private readonly string _objectXPath;
        private readonly List<PropertyMap> _propertyMaps = new List<PropertyMap>();
        private readonly List<LinkedPropertyMap> _linkedPropertyMaps = new List<LinkedPropertyMap>();

        /// <summary>
        /// Initializes a new instance of the ClassMap class.
        /// </summary>
        /// <param name="type">The type of the source object.</param>
        /// <param name="objectXPath">The XPath expression for selecting the object in the XML document.</param>
        /// <param name="propertyMaps">The property maps for the source object.</param>
        /// <param name="linkedPropertyMaps">The linked property maps for the source object.</param>
        public ClassMap(Type type, string objectXPath, List<PropertyMap> propertyMaps,
            List<LinkedPropertyMap> linkedPropertyMaps) :
            this(type, objectXPath)
        {
            _propertyMaps = propertyMaps;
            _linkedPropertyMaps = linkedPropertyMaps;
        }

        /// <summary>
        /// Initializes a new instance of the ClassMap class.
        /// </summary>
        /// <param name="type">The type of the source object.</param>
        /// <param name="objectXPath">The XPath expression for selecting the object in the XML document.</param>
        public ClassMap(Type type, string objectXPath)
        {
            _type = type;
            _objectXPath = objectXPath;
        }

        private PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            if (!(propertyLambda.Body is MemberExpression member))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));
            }

            if (!(member.Member is PropertyInfo propInfo))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));
            }

            Type type = typeof(TSource);
            if (propInfo.ReflectedType != null && type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));
            }

            return propInfo;
        }

        /// <summary>
        /// Adds a property map for a specific property of the source object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpr">The property expression.</param>
        /// <param name="xpath">The XPath expression for selecting the property value in the XML document.</param>
        /// <param name="postConverter">The post-conversion function for the property value (optional).</param>
        /// <returns>The ClassMap instance for method chaining.</returns>
        public ClassMap<TSource> ForProperty<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr, string xpath,
            Func<TProperty, TProperty> postConverter = null)
        {
            var propertyMap = new PropertyMap(GetPropertyInfo(propertyExpr), xpath, postConverter: postConverter);
            _propertyMaps.Add(propertyMap);
            return this;
        }

        /// <summary>
        /// Automatically maps a property to an XML element or attribute based on the property name.
        /// </summary>
        /// <remarks>Generate  xpath selector in this format: PropertyName | @PropertyName</remarks>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpr">The expression representing the property.</param>
        /// <returns>The ClassMap instance for method chaining.</returns>
        public ClassMap<TSource> ForPropertyAuto<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpr);

            string attributeName;
            string elementName = attributeName = propertyInfo.Name;
            string xpathSelector = $"{elementName} | @{attributeName}";

            var propertyMap = new PropertyMap(propertyInfo, xpathSelector);
            _propertyMaps.Add(propertyMap);
            return this;
        }

        public ClassMap<TSource> ForPropertyCustom<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr,
            string xpath, Func<TProperty, TProperty> preConverter)
        {
            var propertyMap = new PropertyMap(GetPropertyInfo(propertyExpr), xpath, preConverter: preConverter);
            _propertyMaps.Add(propertyMap);
            return this;
        }


        /// <summary>
        /// Adds a linked property map for a specific property of the source object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpr">The property expression.</param>
        /// <param name="useDeclaredClassXmlElement">Indicates whether to use the declared class XML element.</param>
        /// <returns>The ClassMap instance for method chaining.</returns>
        public ClassMap<TSource> ForLinkedProperty<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr,
            bool useDeclaredClassXmlElement = false)
        {
            var propertyMap = new LinkedPropertyMap(GetPropertyInfo(propertyExpr), useDeclaredClassXmlElement);
            _linkedPropertyMaps.Add(propertyMap);
            return this;
        }

        /// <summary>
        /// Retrieves the type of the source object.
        /// </summary>
        /// <returns>The type of the source object.</returns>
        public Type GetMappedType() => _type;

        /// <summary>
        /// Retrieves the XPath expression for selecting the object in the XML document.
        /// </summary>
        /// <returns>The XPath expression for selecting the object in the XML document.</returns>
        public string GetObjectXPath() => _objectXPath;

        /// <summary>
        /// Retrieves the property maps for the source object.
        /// </summary>
        /// <returns>The property maps for the source object.</returns>
        public List<PropertyMap> GetPropertyMaps() => _propertyMaps;

        /// <summary>
        /// Retrieves the linked property maps for the source object.
        /// </summary>
        /// <returns>The linked property maps for the source object.</returns>
        public List<LinkedPropertyMap> GetLinkedPropertyMaps() => _linkedPropertyMaps;

        /// <summary>
        /// Creates a new ClassMap instance with the same type, object XPath, property maps, and linked property maps, but with the generic type set to object.
        /// </summary>
        /// <returns>The new ClassMap instance.</returns>
        public ClassMap<object> ToNoneGenericClassMap()
        {
            return new ClassMap<object>(_type, _objectXPath, _propertyMaps, _linkedPropertyMaps);
        }

        public override string ToString()
        {
            return $"[type: {_type}, xpath: {_objectXPath}]";
        }
    }
}