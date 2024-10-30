using XmlMapper.Tests.EqualityComparers;

namespace XmlMapper.Tests.Models
{
    public record User
    {
        public static IEqualityComparer<User> UserComparer { get; } = new UserEqualityComparer();
        public string? FullName { get; set; }
        public string? Login { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public Address? Address { get; set; }
        public List<Role>? Roles { get; set; }
    }
}