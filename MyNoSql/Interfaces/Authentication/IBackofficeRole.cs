using DataServices.MyNoSql.Models.Authentication;

namespace DataServices.MyNoSql.Interfaces.Authentication;

public interface IBackofficeRole
{
    string Id { get; }
    public string Name { get; set; }
    public IEnumerable<PermissionEntity> Permissions { get; set; }
}