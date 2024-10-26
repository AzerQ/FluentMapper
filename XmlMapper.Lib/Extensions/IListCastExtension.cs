using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace XmlMapper.Core.Extensions
{
    public static class IListCastExtension
    {

        private static readonly ConcurrentDictionary<Type, Type> genericListTypes =
            new ConcurrentDictionary<Type, Type>();

        public static IList CastToTyped(this IList list, Type type)
        {
            var listType = genericListTypes.GetOrAdd
                (type, genericType => typeof(List<>).MakeGenericType(new Type[] { genericType }));

            IList otherList = (IList)Activator.CreateInstance(listType);

            foreach (var item in list)
            {
                otherList.Add(item);
            }

            return otherList;
        }
    }
}
