using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XmlMapper.Core.Models
{
    public class ClassMap<TSource>
    {
        private readonly Type _type;
        private readonly string _objectXPath;
        private readonly List<PropertyMap> _propertyMaps = new List<PropertyMap>();
        private readonly List<LinkedPropertyMap> _linkedPropertyMaps = new List<LinkedPropertyMap>();


        public ClassMap(Type type, string objectXPath, List<PropertyMap> propertyMaps,
            List<LinkedPropertyMap> linkedPropertyMaps) :
            this(type, objectXPath)
        {
            _propertyMaps = propertyMaps;
            _linkedPropertyMaps = linkedPropertyMaps;
        }

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
            if (propInfo.ReflectedType != null && type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));
            }

            return propInfo;
        }

        public ClassMap<TSource> ForProperty<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr, string xpath, Func<TProperty, TProperty> postConverter = null)
        {
            var propertyMap = new PropertyMap(GetPropertyInfo(propertyExpr), xpath, postConverter: postConverter);
            _propertyMaps.Add(propertyMap);
            return this;
        }

        public ClassMap<TSource> ForPropertyCustom<TProperty>(Expression<Func<TSource, TProperty>> propertyExpr, string xpath, Func<TProperty, TProperty> preConverter)
        {
            var propertyMap = new PropertyMap(GetPropertyInfo(propertyExpr), xpath, preConverter: preConverter);
            _propertyMaps.Add(propertyMap);
            return this;
        }


        public ClassMap<TSource> ForLinkedProperty<TProperty>(Expression<Func<TSource, IEnumerable<TProperty>>> propertyExpr)
        {
            var propertyMap = new LinkedPropertyMap(GetPropertyInfo(propertyExpr));
            _linkedPropertyMaps.Add(propertyMap);
            return this;
        }

        public Type GetMappedType() => _type;
        public string GetObjectXPath() => _objectXPath;
        public List<PropertyMap> GetPropertyMaps() => _propertyMaps;

        public List<LinkedPropertyMap> GetLinkedPropertyMaps() => _linkedPropertyMaps;

        public ClassMap<object> ToNoneGenericClassMap()
        {
            return new ClassMap<object>(_type, _objectXPath, _propertyMaps, _linkedPropertyMaps);
        }

    }

}
