using XmlMapper.Core.Builders;
using XmlMapper.Core.Models;
using XmlMapper.Tests.Models;

namespace XmlMapper.Tests.MappingConfigurations;

public static class UserMappingConfiguration
{
    private static void AddUserManualConfig(MappingConfigurationBuilder configBuilder)
    {
        configBuilder
            .AddClassConfiguration<User>("/UsersContext/User", classMap =>
            {
                classMap
                    .ForProperty(u => u.FullName, "FullName")
                    .ForProperty(u => u.Login, "Login")
                    .ForProperty(u => u.Age, "Age")
                    .ForProperty(u => u.Bio, "BIO")
                    .ForProperty(u => u.IsActive, "IsActive")
                    .ForProperty(u => u.JoinDate, "JoinDate")
                    .ForLinkedProperty(u => u.Roles, useDeclaredClassXmlElement: true)
                    .ForLinkedProperty(u => u.Address, useDeclaredClassXmlElement: true);

            });
    }
    private static void AddUserAutoConfig(MappingConfigurationBuilder configBuilder)
    {
        configBuilder
            .AddClassConfiguration<User>("/UsersContext/User", classMap =>
            {
                classMap
                    .ForPropertyAuto(u => u.FullName)
                    .ForPropertyAuto(u => u.Login)
                    .ForPropertyAuto(u => u.Age)
                    .ForPropertyAuto(u => u.Bio)
                    .ForPropertyAuto(u => u.IsActive)
                    .ForPropertyAuto(u => u.JoinDate)
                    .ForLinkedProperty(u => u.Roles, useDeclaredClassXmlElement: true)
                    .ForLinkedProperty(u => u.Address, useDeclaredClassXmlElement: true);

            });
    }
    private static void AddRolesConfiguration(MappingConfigurationBuilder configBuilder)
    {
        configBuilder
            .AddClassConfiguration<Role>("//Roles/Role", classMap =>
            {
                classMap
                    .ForProperty(r => r.Id, "@id")
                    .ForProperty(r => r.Name, "@name")
                    .ForProperty(r => r.Description, "@description");
            });
    }
    private static void AddAddressConfiguration(MappingConfigurationBuilder configBuilder)
    {
        configBuilder
            .AddClassConfiguration<Address>("//Address", classMap =>
            {
                classMap
                    .ForProperty(a => a.City, "City")
                    .ForProperty(a => a.PostalCode, "PostalCode")
                    .ForProperty(a => a.Street, "Street");
            });
    }
    public static MappingConfiguration GetUserMappingConfiguration()
    {
        var configBuilder = new MappingConfigurationBuilder()
            .SetConfigurationName("UserManualConfiguration");

        AddUserManualConfig(configBuilder);
        AddRolesConfiguration(configBuilder);
        AddAddressConfiguration(configBuilder);

        return configBuilder.Build();
        
    }
    public static MappingConfiguration GetUserAutoMappingConfiguration()
    {
        var configBuilder = new MappingConfigurationBuilder()
                .SetConfigurationName("UserAutoConfiguration");
        
        AddUserAutoConfig(configBuilder);
        AddRolesConfiguration(configBuilder);
        AddAddressConfiguration(configBuilder);

        return configBuilder.Build();
    }
}