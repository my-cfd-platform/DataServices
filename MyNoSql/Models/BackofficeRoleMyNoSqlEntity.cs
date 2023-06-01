using DataServices.Models.Auth.Permissions;
using DataServices.Models.Auth.Roles;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class BackofficeRoleMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeRole
{
    public static string GeneratePartitionKey() => "br";

    public static string GenerateRowKey(string id) => id;

    public string Id => this.RowKey;

    public string Name { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }

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
            Permissions = src.Permissions,
        };
    }

    public static BackofficeRoleModel ToDomain(IBackofficeRole src)
    {
        return new BackofficeRoleModel
        {
            Id = src.Id,
            Name = src.Name,
            Permissions = src.Permissions,
        };
    }
}