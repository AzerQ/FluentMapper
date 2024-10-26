using XmlMapper.Core;
using XmlMapper.Core.Builders;

namespace XmlMapper.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new MappingConfigurationBuilder();

            configBuilder
                .AddClassConfiguration<User>("/UsersContext/User", classMap =>
                {
                    classMap
                        .ForProperty(u => u.FullName, "FullName")
                        .ForProperty(u => u.Age, "Age", age => age + 2)
                        .ForProperty(u => u.BIO, "BIO", bio => "Some sheet: " + bio)
                        .ForProperty(u => u.Login, "Login")
                        .ForLinkedProperty(u => u.Roles);
                })
                .AddClassConfiguration<Role>("/UsersContext/Roles/Role", classMap =>
                {
                    classMap
                        .ForProperty(r => r.ID, "@id")
                        .ForProperty(r => r.Name, "@name")
                        .ForProperty(r => r.Description, "@description");
                });
                

            var mappingConfig = configBuilder.Build();

            var xmlString = XmlTest.UserContext;

            IXmlMapper xmlMapper = XmlMapperFactory.DefaultXmlMapper;

            User user = xmlMapper.MapToObject<User>(mappingConfig, xmlString);

        }
    }

    class User
    {
        public string FullName { get; set; }
        public string Login { get; set; }
        public int Age { get; set; }
        public string BIO { get; set; }
        public List<Role> Roles { get; set; }
    }

    class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
