using System.Collections;

namespace XmlMapper.ConsoleApp
{
    internal static class IListCastExtension
    {
        public static IList CastToTyped(this Type type)
        {
            var listType = typeof(List<>).MakeGenericType(new Type[] { type });
            IList list = (IList)Activator.CreateInstance(listType);
            return list;
        }
    }
}
