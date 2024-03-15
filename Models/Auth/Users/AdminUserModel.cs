using System.ComponentModel.DataAnnotations;
using DataServices.MyNoSql.Interfaces.Authentication;

namespace DataServices.Models.Auth.Users
{
    public class AdminUserModel : IAdminUser
    {
        [Required]
        public string Id { get; set; }
        public DateTime Registered { get; set; }
        public bool IsBlocked { get; set; }
        public string PersonalName { get; set; }
        public IEnumerable<string> CertAliases { get; set; } = new List<string>();
    }
}