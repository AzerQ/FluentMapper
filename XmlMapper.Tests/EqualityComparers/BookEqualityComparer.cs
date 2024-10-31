using XmlMapper.Tests.Models;
using XmlMapper.Tests.Utils;

namespace XmlMapper.Tests.EqualityComparers;

public sealed class BookEqualityComparer : IEqualityComparer<Book>
{
    public bool Equals(Book? x, Book? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return string.Equals(x.Title, y.Title, StringComparison.InvariantCultureIgnoreCase)
               && string.Equals(x.Author, y.Author, StringComparison.InvariantCultureIgnoreCase)
               && x.Year == y.Year
               && CollectionComparer.CollectionsEquals(x.Genres, y.Genres);
    }

    public int GetHashCode(Book obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Title, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Author, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Year);
        hashCode.Add(obj.Genres);
        return hashCode.ToHashCode();
    }
}
