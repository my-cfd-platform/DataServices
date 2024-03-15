namespace DataServices.MyNoSql.Interfaces.Authentication;

public interface IAdminUser
{
    string Id { get; set; }
    public DateTime Registered { get; set; }
    public bool IsBlocked { get; set; }
    public string PersonalName { get; set; }
    public IEnumerable<string> CertAliases { get; set; }
}