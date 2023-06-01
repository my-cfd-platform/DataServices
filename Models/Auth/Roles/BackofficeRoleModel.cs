using DataServices.Models.Auth.Permissions;

namespace DataServices.Models.Auth.Roles
{
    public class BackofficeRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();

        public static BackofficeRoleModel Create(BackofficeRoleModel src)
        {
            return new BackofficeRoleModel
            {
                Id = src.Id,
                Name = src.Name,
            };
        }

        public bool HasViewPermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasViewAction()) ?? false;

        public bool HasEditPermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasEditAction()) ?? false;

        public bool HasDeletePermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasDeleteAction()) ?? false;

        public Permission[] GetPermissionsLinkedTo(PermissionResource resource)
            => Permissions?.Where(p => p.Resource.GetInfo().LinkedTo == resource).ToArray() ?? Array.Empty<Permission>();
        
        public bool HasAddPermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasAddAction()) ?? false;
        
        public bool HasDownloadPermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasDownloadAction()) ?? false;
        
        public bool HasImportPermission(PermissionResource resource)
            => Permissions?.Any(p => p.Resource == resource && p.HasImportAction()) ?? false;
    }
}