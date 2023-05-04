using DataServices.Extensions;
using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class BackofficeTeamMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeTeam
{
    //todo fix to bt
    public static string GeneratePartitionKey() => "bu";

    public static string GenerateRowKey(string id) => id;

    public string Id
    {
        get => this.RowKey;
        set => this.RowKey = value;
    }

    public string Name { get; set; }
    public IEnumerable<string> OfficeIds { get; set; }
    public string TeamLeadId { get; set; }
    public TeamType Type { get; set; }

    public static BackofficeTeamMyNoSqlEntity Create(IBackofficeTeam src)
    {
        if(src.Id.IsNullOrEmpty()) 
            src.Id = Guid.NewGuid().ToString();
        return new BackofficeTeamMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Name = src.Name,
            OfficeIds = src.OfficeIds,
            TeamLeadId = src.TeamLeadId,
            Type = src.Type,
        };
    }



    /*
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
            InternalPhoneNumberId = src.PhoneNumberIds.FirstOrDefault(n => n.Key == (int) DataServices.Services.Enums.CallProviderType.Samcon).Value,
            AssignedPhonePoolIds = src.AssignedPhonePoolIds ,
            AsteriskPhoneNumberId = src.PhoneNumberIds.FirstOrDefault(n => n.Key == (int) DataServices.Services.Enums.CallProviderType.Asterisk).Value,
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
            PersonalName = src.PersonalName,
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
    */
}