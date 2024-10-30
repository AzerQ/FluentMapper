namespace XmlMapper.Tests.Models;

public static class ModelManualCreator
{
    public static User CreateUserModel()
    {
        return new User()
        {
            Address = new Address
            {
                City = "Springfield",
                PostalCode = "12345",
                Street = "Main Street"
            },
            Bio = "Software Developer",
            Age = 30,
            Login = "alice.smith",
            Roles =
            [
                new Role { Id = 101, Name = "Admin", Description = "Full access" },
                new Role { Id = 102, Name = "User", Description = "Limited access" }
            ],
            FullName = "Alice Smith",
            IsActive = true,
            JoinDate = new DateTime(2022, 03, 15)
        };
    }

    public static List<Book> CreateBooksList()
    {
        return
        [
            new Book
            {
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Year = 1925,
                Genres = [new Genre {Name = "Novel"}, new Genre {Name = "Fiction"}]
            },
            new Book
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Year = 1960,
                Genres = [new Genre {Name = "Classic"}, new Genre {Name = "Drama"}]
            }
        ];
    }
}