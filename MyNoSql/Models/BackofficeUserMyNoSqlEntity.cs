using DataServices.Extensions;
using DataServices.Models.Auth.Roles;
using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class BackofficeUserMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeUser
{
    public static string GeneratePartitionKey() => "bu";

    public static string GenerateRowKey(string id) => id;

    public string Id => this.RowKey;

    public DateTime Registered { get; set; }
    public bool IsBlocked { get; set; }
    public string PersonalName { get; set; }
    public bool IsAdmin { get; set; }
    public string ReferralLink { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IEnumerable<string> CertAliases { get; set; }
    public string InternalPhoneNumberId { get; set; }
    public IEnumerable<string> AssignedPhonePoolIds { get; set; }
    public string AsteriskPhoneNumberId { get; set; }
    public string TeamId { get; set; }
    public int DataAccessRules { get; set; }
    public int? SkillLevel { get; set; }

    public static BackofficeUserMyNoSqlEntity Create(IBackofficeUser src)
    {
        return new BackofficeUserMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Registered = src.Registered,
            IsBlocked = src.IsBlocked ,
            PersonalName = src.PersonalName ,
            IsAdmin = src.IsAdmin ,
            ReferralLink = src.ReferralLink,
            Roles = src.Roles ,
            CertAliases = src.CertAliases ,
            InternalPhoneNumberId = src.InternalPhoneNumberId ,
            AssignedPhonePoolIds = src.AssignedPhonePoolIds ,
            AsteriskPhoneNumberId = src.AsteriskPhoneNumberId ,
            TeamId = src.TeamId ,
            DataAccessRules = src.DataAccessRules ,
            SkillLevel = src.SkillLevel
        };
    }

    public static BackofficeUserMyNoSqlEntity Create(IBackOfficeUser src)
    {
        return new BackofficeUserMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Registered = src.Registered,
            IsBlocked = src.IsBlocked ,
            PersonalName = src.PersonalName ,
            IsAdmin = src.IsAdmin ,
            ReferralLink = src.ReferralLink,
            Roles = src.Roles.Select(r=>r.Id) ,
            CertAliases = src.CertAliases ,
            InternalPhoneNumberId = src.PhoneNumberIds.FirstOrDefault(n => n.Key == (int) CallProviderType.Samcon).Value,
            AssignedPhonePoolIds = src.AssignedPhonePoolIds ,
            AsteriskPhoneNumberId = src.PhoneNumberIds.FirstOrDefault(n => n.Key == (int) CallProviderType.Asterisk).Value,
            TeamId = (string.IsNullOrEmpty(src.TeamId) ? null : src.TeamId)!,
            DataAccessRules = (int) src.DataAccessRules,
            SkillLevel = src.SkillLevel.HasValue ? (int) src.SkillLevel : null
        };
    }

    public static BackOfficeUserModel ToDomain(IBackofficeUser src, IEnumerable<BackofficeRoleModel> roles)
    {
        return new()
        {
            Roles = GetUserRoles(roles, src),
            Id = src.Id,
            Registered = src.Registered,
            IsBlocked = src.IsBlocked,
            PersonalName = src.PersonalName.IsNotNullOrEmpty() ? src.PersonalName : src.Id,
            IsAdmin = src.IsAdmin,
            ReferralLink = src.ReferralLink,
            CertAliases = src.CertAliases,
            AssignedPhonePoolIds = src.AssignedPhonePoolIds,
            TeamId = src.TeamId,
            DataAccessRules = (UserDataAccessRules)src.DataAccessRules,
            SkillLevel = src.SkillLevel.HasValue ? (UserSkillLevel)src.SkillLevel : null,
            PhoneNumberIds = new Dictionary<int, string>
            {
                {(int) CallProviderType.Samcon, src.InternalPhoneNumberId},
                {(int) CallProviderType.Asterisk, src.AsteriskPhoneNumberId}
            }
        };
    }

    private static IEnumerable<BackofficeRoleModel> GetUserRoles(IEnumerable<BackofficeRoleModel> roles, IBackofficeUser user)
    {
        return roles.Where(r => user.Roles.Contains(r.Id));
    }

}