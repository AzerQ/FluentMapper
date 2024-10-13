using System;
using System.Collections;
using System.Collections.Generic;

namespace XmlMapper
{
    public static class IListCastExtension
    {
        public static IList CastToTyped(this IList list, Type type)
        {
            var listType = typeof(List<>).MakeGenericType(new Type[] { type });
            IList otherList = (IList)Activator.CreateInstance(listType);
            foreach (var item in list) {
                otherList.Add(item);
            }

            return otherList;
        }
    }
}
