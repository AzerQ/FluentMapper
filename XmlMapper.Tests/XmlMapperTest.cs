using XmlMapper.Core;
using XmlMapper.Core.Models;
using XmlMapper.Tests.MappingConfigurations;
using XmlMapper.Tests.Models;

namespace XmlMapper.Tests
{
    [TestClass]
    public class XmlMapperTest
    {
        private static string UsersContextXml => XmlData.UsersContext;
        
        [TestMethod]
        public void MapToObject_ShouldMapUserProperties()
        {
            IXmlMapper xmlMapper = XmlMapperFactory.DefaultXmlMapper;
            MappingConfiguration usersMappingConfiguration = UserMappingConfiguration.GetUsersMappingConfiguration();
            
            User mappedUser = xmlMapper.MapToObject<User>(usersMappingConfiguration, UsersContextXml);
            User manualCreatedUser = ModelManualCreator.CreateUserModel();
            Assert.AreEqual(manualCreatedUser, mappedUser, User.UserComparer,
                "Manual created user and mapped user not equals!");
        }
        
    }
}