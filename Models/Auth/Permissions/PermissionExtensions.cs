using System.Reflection;

namespace DataServices.Models.Auth.Permissions
{
    public static class PermissionExtensions
    {
        public static PermissionResourceInfo GetInfo(this PermissionResource @enum)
        {
            var member = typeof(PermissionResource).GetMember(@enum.ToString());
            var info = member[0].GetCustomAttribute<PermissionResourceInfo>();

            if (info == null) throw new Exception();

            return info;
        }
        
        public static bool IsObsolete(this PermissionResource @enum)
        {
            var member = typeof(PermissionResource).GetMember(@enum.ToString());
            var obsoleteAttribute = member[0].GetCustomAttribute<ObsoleteAttribute>();

            return obsoleteAttribute != null;
        }
        
        public static PermissionResource[] GetLinked(this PermissionResource @enum)
        {
            return Enum.GetValues(typeof(PermissionResource))
                .Cast<PermissionResource>()
                .Where(x => !x.IsObsolete() && x.GetInfo().LinkedTo == @enum)
                .ToArray();
        }
    }
}