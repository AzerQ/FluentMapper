using XmlMapper.Core.Builders;
using XmlMapper.Core.Models;
using XmlMapper.Tests.Models;

namespace XmlMapper.Tests.MappingConfigurations;

public static class UserMappingConfiguration
{
    public static MappingConfiguration GetUsersMappingConfiguration()
    {
        var configBuilder = new MappingConfigurationBuilder();

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
                    .ForLinkedProperty(u => u.Roles)
                    .ForLinkedProperty(u => u.Address);

            })
            .AddClassConfiguration<Role>("/UsersContext/User/Roles/Role", classMap =>
            {
                classMap
                    .ForProperty(r => r.Id, "@id")
                    .ForProperty(r => r.Name, "@name")
                    .ForProperty(r => r.Description, "@description");
            })
            .AddClassConfiguration<Address>("/UsersContext/User/Address", classMap =>
            {
                classMap
                    .ForProperty(a => a.City, "City")
                    .ForProperty(a => a.PostalCode, "PostalCode")
                    .ForProperty(a => a.Street, "Street");
            });

        return configBuilder.Build();
    }
}