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
}