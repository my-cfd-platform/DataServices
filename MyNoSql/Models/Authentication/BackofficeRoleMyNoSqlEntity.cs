using DataServices.Models.Auth.Permissions;
using DataServices.Models.Auth.Roles;
using DataServices.MyNoSql.Interfaces.Authentication;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models.Authentication;

public class BackofficeRoleMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeRole
{
    public static string GeneratePartitionKey() => "br";

    public static string GenerateRowKey(string id) => id;

    public string Id => RowKey;

    public string Name { get; set; }
    public IEnumerable<PermissionEntity> Permissions { get; set; }

    public static BackofficeRoleMyNoSqlEntity Create(IBackofficeRole src)
    {
        return new BackofficeRoleMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Name),
            Name = src.Name,
            Permissions = src.Permissions,
        };
    }

    public static BackofficeRoleMyNoSqlEntity Create(BackofficeRoleModel src)
    {
        return new BackofficeRoleMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Name),
            Name = src.Name,
            Permissions = src.Permissions
                .Select(p => new PermissionEntity
                {
                    Resource = p.Resource.ToString(),
                    Action = p.Actions
                }),
        };
    }

    public static BackofficeRoleModel ToDomain(IBackofficeRole src)
    {
        return new BackofficeRoleModel
        {
            Id = src.Id,
            Name = src.Name,
            Permissions = src.Permissions
                .Select(p =>
                {
                    return new Permission(Enum.Parse<PermissionResource>(p.Resource), p.Action);
                }),
        };
    }
}

public class PermissionEntity
{
    public string Resource { get; set; }
    public PermissionActions Action { get; set; }
}