using XmlMapper.Tests.Models;

namespace XmlMapper.Tests.EqualityComparers;

public sealed class UserEqualityComparer : IEqualityComparer<User>
{
    public bool Equals(User? x, User? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return string.Equals(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase) 
               && string.Equals(x.Login, y.Login, StringComparison.InvariantCultureIgnoreCase) 
               && x.Age == y.Age 
               && string.Equals(x.Bio, y.Bio, StringComparison.InvariantCultureIgnoreCase) 
               && x.IsActive == y.IsActive 
               && x.JoinDate.Equals(y.JoinDate) 
               && Equals(x.Address, y.Address) 
               && x.Roles != null && y.Roles!= null
               && x.Roles.SequenceEqual(y.Roles);
    }

    public int GetHashCode(User obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.FullName, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Login, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.Age);
        hashCode.Add(obj.Bio, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(obj.IsActive);
        hashCode.Add(obj.JoinDate);
        hashCode.Add(obj.Address);
        hashCode.Add(obj.Roles);
        return hashCode.ToHashCode();
    }
}