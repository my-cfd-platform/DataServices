using DataServices.MyNoSql.Models;

namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeRole
{
    string Id { get; }
    public string Name { get; set; }
    public IEnumerable<PermissionEntity> Permissions { get; set; }
}