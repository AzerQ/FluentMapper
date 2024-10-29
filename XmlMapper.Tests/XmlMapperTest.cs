using XmlMapper.Core;
using XmlMapper.Core.Builders;
using XmlMapper.Core.Models;
using XmlMapper.Tests.Models;

namespace XmlMapper.Tests
{
    [TestClass]
    public class XmlMapperTest
    {
        private static string UsersContextXml => XmlData.UsersContext;
        
        private MappingConfiguration _usersMappingConfiguration = null!;

        public XmlMapperTest()
        {
            TestsSetUp();
        }

        private void TestsSetUp()
        {
            _usersMappingConfiguration = GetUsersMappingConfiguration();
        }

        private static MappingConfiguration GetUsersMappingConfiguration()
        {
            var configBuilder = new MappingConfigurationBuilder();

            configBuilder
                .AddClassConfiguration<User>("/UsersContext/User", classMap =>
                {
                    classMap
                        .ForProperty(u => u.FullName, "FullName")
                        .ForProperty(u => u.Login, "Login")
                        .ForProperty(u => u.Age, "Age", age => age + 2)
                        .ForProperty(u => u.BIO, "BIO", bio => "Some sheet: " + bio)
                        .ForLinkedProperty(u => u.Roles);
                })
                .AddClassConfiguration<Role>("/UsersContext/User/Roles/Role", classMap =>
                {
                    classMap
                        .ForProperty(r => r.ID, "@id")
                        .ForProperty(r => r.Name, "@name")
                        .ForProperty(r => r.Description, "@description");
                });

            return configBuilder.Build();
        }

        [TestMethod]
        public void MapToObject_ShouldMapUserProperties()
        {
            var xmlMapper = XmlMapperFactory.DefaultXmlMapper;
            var user = xmlMapper.MapToObject<User>(_usersMappingConfiguration, UsersContextXml);

            Assert.AreEqual("Alice Smith", user.FullName);
            Assert.AreEqual("alice.smith", user.Login);
            Assert.AreEqual(32, user.Age); // Применяется трансформация age => age + 2
            Assert.AreEqual("Some sheet: Software Developer",
                user.BIO); // Применяется трансформация bio => "Some sheet: " + bio
        }
        
        [TestMethod]
        public void MapToObject_ShouldMapNestedRoles()
        {
            var xmlMapper = XmlMapperFactory.DefaultXmlMapper;
            var user = xmlMapper.MapToObject<User>(_usersMappingConfiguration, UsersContextXml);

            Assert.IsNotNull(user.Roles);
            Assert.AreEqual(2, user.Roles.Count);

            // Проверка первой роли
            Assert.AreEqual(101, user.Roles[0].ID);
            Assert.AreEqual("Admin", user.Roles[0].Name);
            Assert.AreEqual("Full access", user.Roles[0].Description);

            // Проверка второй роли
            Assert.AreEqual(102, user.Roles[1].ID);
            Assert.AreEqual("User", user.Roles[1].Name);
            Assert.AreEqual("Limited access", user.Roles[1].Description);
        }
        
     
    }
}