using DataServices.Models.Auth.Permissions;
using DataServices.Models.Auth.Roles;

namespace DataServices.Models.Auth.Users
{
    public class BackOfficeUserModel : IBackOfficeUser
    {
        public string Id { get; set; }
        public DateTime Registered { get; set; }
        public bool IsBlocked { get; set; }
        public string PersonalName { get; set; }
        public bool IsAdmin { get; set; }
        public string ReferralLink { get; set; }
        public IEnumerable<BackofficeRoleModel> Roles { get; set; } = new List<BackofficeRoleModel>();
        public IEnumerable<string> CertAliases { get; set; } = new List<string>();
        public IEnumerable<string> AssignedPhonePoolIds { get; set; } = new List<string>();
        public string TeamId { get; set; }
        public UserDataAccessRules DataAccessRules { get; set; }
        public UserSkillLevel? SkillLevel { get; set; }
        public Dictionary<int, string> PhoneNumberIds { get; set; }

        public bool CanView(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasViewPermission(resource));
        }

        public IEnumerable<Permission> GetPermissionsLinkedTo(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return new List<Permission>();
            }

            if (IsAdmin)
            {
                return resource.GetLinked()
                    .Select(r => new Permission(r,
                        PermissionActions.Edit | PermissionActions.Delete | PermissionActions.Delete |
                        PermissionActions.Download | PermissionActions.Add));
            }

            if (Roles == null)
            {
                return new List<Permission>();
            }

            return Roles.SelectMany(x => x.GetPermissionsLinkedTo(resource));
        }

        public bool CanEdit(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasEditPermission(resource));
        }

        public bool CanDelete(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasDeletePermission(resource));
        }

        public string GetPhoneNumberId(int callProvider)
        {
            return PhoneNumberIds
                .FirstOrDefault(n => n.Key == callProvider).Value;
        }

        public bool CanAdd(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasAddPermission(resource));
        }
        
        public bool CanDownload(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasDownloadPermission(resource));
        }
        
        public bool CanImport(PermissionResource resource)
        {
            if (IsBlocked)
            {
                return false;
            }

            if (IsAdmin)
            {
                return true;
            }

            if (Roles == null)
            {
                return false;
            }

            return Roles.Any(x => x.HasImportPermission(resource));
        }
    }
}