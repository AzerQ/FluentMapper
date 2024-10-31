using XmlMapper.Tests.EqualityComparers;

namespace XmlMapper.Tests.Models;

public record Book
{

    public static IEqualityComparer<Book> BookComparer { get; } = new BookEqualityComparer();

    public string? Title { get; set; }
    public string? Author { get; set; }
    public int Year { get; set; }
    public List<Genre>? Genres { get; set; }
}