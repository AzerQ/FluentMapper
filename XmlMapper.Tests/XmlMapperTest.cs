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
        private static string LibraryContextXml => XmlData.LibraryContext;

        [TestMethod]
        public void MapToObject_ShouldMapAllProperties()
        {
            IXmlMapper xmlMapper = XmlMapperFactory.DefaultXmlMapper;
            MappingConfiguration usersMappingConfig = UserMappingConfiguration.GetUsersMappingConfiguration();

            User mappedUser = xmlMapper.MapToObject<User>(usersMappingConfig, UsersContextXml);
            User manualCreatedUser = ModelManualCreator.CreateUserModel();
            
            Assert.AreEqual(manualCreatedUser, mappedUser, User.UserComparer,
                "Manual created user and mapped user not equals!");
        }

        [TestMethod]
        public void MapToCollection_ShouldMapAllObjects()
        {
            IXmlMapper xmlMapper = XmlMapperFactory.DefaultXmlMapper;
            MappingConfiguration bookMappingConfig = BookMappingConfiguration.GetBookMappingConfiguration();

            List<Book> mappedBooks = xmlMapper.MapToCollection<Book>(bookMappingConfig, LibraryContextXml);
            List<Book> manualCreatedBooks = ModelManualCreator.CreateBooksList();

            bool collectionEquals = CollectionComparer.CollectionsEquals(mappedBooks, manualCreatedBooks, Book.BookComparer);
            
            Assert.IsTrue(collectionEquals, "Manual created collection not equals mapped books collection!");
            
        }
    }
}