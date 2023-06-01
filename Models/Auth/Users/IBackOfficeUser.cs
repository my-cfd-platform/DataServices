using DataServices.Models.Auth.Permissions;
using DataServices.Models.Auth.Roles;

namespace DataServices.Models.Auth.Users
{
    public interface IBackOfficeUser
    {
        public string Id { get; set; }
        public DateTime Registered { get; set; }
        public bool IsBlocked { get; set; }
        public string PersonalName { get; set; }
        public bool IsAdmin { get; set; }
        public string ReferralLink { get; set; }
        public IEnumerable<BackofficeRoleModel> Roles { get; set; }
        public IEnumerable<string> CertAliases { get; set; }
        public IEnumerable<string> AssignedPhonePoolIds { get; set; }
        public string TeamId { get; set; }
        public UserDataAccessRules DataAccessRules { get; set; }
        public UserSkillLevel? SkillLevel { get; set; }
        public Dictionary<int, string> PhoneNumberIds { get; set; }

        public bool CanView(PermissionResource resource);
        public IEnumerable<Permission> GetPermissionsLinkedTo(PermissionResource resource);
        public bool CanEdit(PermissionResource resource);
        public bool CanDelete(PermissionResource resource);
        public string GetPhoneNumberId(int callProvider);
        public bool CanAdd(PermissionResource resource);
        bool CanDownload(PermissionResource resource);
        bool CanImport(PermissionResource resource);
    }
}