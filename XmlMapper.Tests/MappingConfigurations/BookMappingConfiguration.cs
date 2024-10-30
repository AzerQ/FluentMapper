using XmlMapper.Core.Builders;
using XmlMapper.Core.Models;
using XmlMapper.Tests.Models;

namespace XmlMapper.Tests.MappingConfigurations;

public static class BookMappingConfiguration
{
    public static MappingConfiguration GetBookMappingConfiguration()
    {
        var configBuilder = new MappingConfigurationBuilder();

        configBuilder
            .AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
            {
                classMap
                    .ForProperty(b => b.Title, "Title")
                    .ForProperty(b => b.Author, "Author")
                    .ForProperty(b => b.Year, "Year")
                    .ForLinkedProperty(b => b.Genres, useDeclaredClassXmlElement: true);
            })
            .AddClassConfiguration<Genre>("//Genres/Genre", classMap =>
            {
                classMap
                    .ForProperty(g => g.Name, "@name");
            });
        
        return configBuilder.Build();
    }
}