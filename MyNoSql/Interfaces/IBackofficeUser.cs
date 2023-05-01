namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeUser
{
    string Id { get; }
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
}