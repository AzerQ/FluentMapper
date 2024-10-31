# XmlMapper

`XmlMapper` — это библиотека для удобного маппинга данных из XML на C#-классы с использованием XPath-селекторов. Поддерживает гибкую настройку маппинга, включая преобразование значений и вложенные объекты.

## Установка

Для начала, добавьте проект как зависимость в ваше приложение.

```bash
# Предполагаемая команда для установки (например, через NuGet)
dotnet add package XmlMapper.Fluent
```

## Быстрый старт

Библиотека позволяет настроить маппинг между XML-данными и C#-классами через `MappingConfigurationBuilder`, задавая маппинг свойств и вложенных объектов.

### Подготовка данных и сущностей

1. Нам поступают данные в формате XML следующего вида:
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
2. Объявим сущности в коде, для того чтобы можно было работать с данными:
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

### Настройка маппинга
Чтобы связать XML с классами, используйте `MappingConfigurationBuilder`:
```csharp
var configBuilder = new MappingConfigurationBuilder();

configBuilder
    .AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
    {
        classMap
            .ForProperty(b => b.Title, "Title")
            .ForProperty(b => b.Author, "Author")
            .ForProperty(b => b.Year, "Year", year => year + 1) // преобразование значения
            .ForProperty(b => b.Synopsis, "Synopsis", syn => "Synopsis: " + syn)
            .ForLinkedProperty(b => b.Genres); // маппинг для вложенного списка
    })
    .AddClassConfiguration<Genre>("/LibraryContext/Book/Genres/Genre", classMap =>
    {
        classMap
            .ForProperty(g => g.Name, "@name");
    });

var mappingConfig = configBuilder.Build();
```

### Маппинг XML данных к объекту
После настройки можно вызвать метод `MapToObject`, чтобы получить экземпляр `Book` с заполненными данными из XML:
```csharp
var xmlMapper = XmlMapperFactory.DefaultXmlMapper;
Book book = xmlMapper.MapToObject<Book>(mappingConfig, xmlString);
```

## Примеры использования
### Простое сопоставление без преобразования
Для базового маппинга свойств используйте `ForProperty`:
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

### Сопоставление с преобразованием значений
Чтобы выполнить преобразование значения перед маппингом, передайте функцию преобразования:
```csharp
configBuilder.AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
{
    classMap
        .ForProperty(b => b.Year, "Year", year => year + 1) // добавляет 1 к году
        .ForProperty(b => b.Synopsis, "Synopsis", syn => syn.ToUpper()); // переводим в верхний регистр
});
```

### Маппинг вложенных объектов
Для маппинга вложенных объектов используйте `ForLinkedProperty`:

```csharp
configBuilder.AddClassConfiguration<Book>("/LibraryContext/Book", classMap =>
{
    classMap
        .ForLinkedProperty(b => b.Genres); // ссылка на другой класс
});

configBuilder.AddClassConfiguration<Genre>("/LibraryContext/Book/Genres/Genre", classMap =>
{
    classMap
        .ForProperty(g => g.Name, "@name");
});
```

### Поддержка списков объектов
Библиотека поддерживает маппинг в списки объектов:
```csharp
List<Book> books = xmlMapper.MapToCollection<Book>(mappingConfig, xmlString);
```