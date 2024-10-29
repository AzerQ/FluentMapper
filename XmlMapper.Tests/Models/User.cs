namespace XmlMapper.Tests.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Login { get; set; }
        public int Age { get; set; }
        public string BIO { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public Address Address { get; set; }
        public List<Role> Roles { get; set; }
    }

}
