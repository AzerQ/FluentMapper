# XmlMapper

`XmlMapper` is a library for convenient mapping of data from XML to C# classes using XPath selectors. It supports flexible mapping configuration, including value transformations and nested objects.

## Installation

First, add the project as a dependency in your application.

```bash
# Assumed command for installation (e.g., via NuGet)
dotnet add package XmlMapper
```

## Quick Start

The library allows you to configure the mapping between XML data and C# classes through `MappingConfigurationBuilder`, specifying the mapping of properties and nested objects.

### Preparing Data and Entities

1. We receive data in XML format as follows:
```xml
<LibraryContext>
    <Book>
        <Title>The Great Gatsby</Title>
        <Author>F. Scott Fitzgerald</Author>
        <Year>1925</Year>
        <Synopsis>A story about the jazz age.</Synopsis>
        <Genres>
            <Genre name="Novel"/>
            <Genre name="Fiction"/>
        </Genres>
    </Book>
</LibraryContext>
```
2. Let's declare the entities in code so that we can work with the data:
```csharp
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Synopsis { get; set; }
    public List<Genre> Genres { get; set; }
}

public class Genre
{
    public string Name { get; set; }
}
```

### Mapping Configuration
To link XML with classes, use `MappingConfigurationBuilder`:
```csharp
var configBuilder = new MappingConfigurationBuilder();

configBuilder
    .AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
    {
        classMap
            .ForProperty(b => b.Title, "Title")
            .ForProperty(b => b.Author, "Author")
            .ForProperty(b => b.Year, "Year", year => year + 1) // value transformation
            .ForProperty(b => b.Synopsis, "Synopsis", syn => "Synopsis: " + syn)
            .ForLinkedProperty(b => b.Genres); // mapping for nested list
    })
    .AddClassConfiguration<Genre>("/LibraryContext/Book/Genres/Genre", classMap =>
    {
        classMap
            .ForProperty(g => g.Name, "@name");
    });

var mappingConfig = configBuilder.Build();
```

### Mapping XML Data to Object
After configuration, you can call the `MapToObject` method to get an instance of `Book` with data filled from XML:
```csharp
var xmlMapper = XmlMapperFactory.DefaultXmlMapper;
Book book = xmlMapper.MapToObject<Book>(mappingConfig, xmlString);
```

## Usage Examples
### Simple Mapping Without Transformation
For basic property mapping, use `ForProperty`:
```csharp
configBuilder.AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
{
    classMap
        .ForProperty(b => b.Title, "Title")
        .ForProperty(b => b.Author, "Author")
        .ForProperty(b => b.Year, "Year")
        .ForProperty(b => b.Synopsis, "Synopsis");
});
```
### Mapping with Value Transformation
To perform value transformation before mapping, pass a transformation function:
```csharp
configBuilder.AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
{
    classMap
        .ForProperty(b => b.Year, "Year", year => year + 1) // adds 1 to the year
        .ForProperty(b => b.Synopsis, "Synopsis", syn => syn.ToUpper()); // converts to uppercase
});
```

### Mapping Nested Objects
To map nested objects, use `ForLinkedProperty`:

```csharp
configBuilder.AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
{
    classMap
        .ForLinkedProperty(b => b.Genres); // reference to another class
});

configBuilder.AddClassConfiguration<Genre>("/LibraryContext/Book/Genres/Genre", classMap =>
{
    classMap
        .ForProperty(g => g.Name, "@name");
});
```

### Support for Object Lists
The library supports mapping to lists of objects:
```csharp
List<Book> books = xmlMapper.MapToCollection<Book>(mappingConfig, xmlString);
```