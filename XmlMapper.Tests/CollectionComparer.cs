using System.Diagnostics;

namespace XmlMapper.Tests;

public static class CollectionComparer
{
    public static bool CollectionsEquals<T>(IEnumerable<T>? firstCollection,
                                            IEnumerable<T>? secondCollection, IEqualityComparer<T>? elementComparer = default)
    {
        bool allCollectionsIsNulls = firstCollection is null && secondCollection is null;
        bool anyCollectionIsNull = firstCollection is null || secondCollection is null;
        
        if (allCollectionsIsNulls)
            return true;

        if (anyCollectionIsNull)
            return false;

        Debug.Assert(firstCollection != null, nameof(firstCollection) + " != null");
        Debug.Assert(secondCollection != null, nameof(secondCollection) + " != null");
        
        return firstCollection.SequenceEqual(secondCollection, elementComparer);
    }
}