using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace XmlMapper.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for casting lists to typed lists.
    /// </summary>
    public static class IListCastExtension
    {
        private static readonly ConcurrentDictionary<Type, Type> genericListTypes =
           new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// Creates a new typed list by casting the elements of the current list to the specified type.
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <param name="type">The type to cast the elements to.</param>
        /// <returns>A new typed list containing the cast elements.</returns>
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
