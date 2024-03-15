using DataServices.Extensions;
using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Interfaces.Authentication;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models.Authentication;

public class AdminUserMyNoSqlEntity : MyNoSqlDbEntity, IAdminUser
{
    public static string GeneratePartitionKey() => "au";

    public static string GenerateRowKey(string id) => id;

    public string Id
    {
        get => RowKey;
        set => RowKey = value;
    }

    public DateTime Registered { get; set; }
    public bool IsBlocked { get; set; }
    public string PersonalName { get; set; }

    public IEnumerable<string> CertAliases { get; set; }

    public static AdminUserMyNoSqlEntity Create(IAdminUser src)
    {
        return new AdminUserMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Registered = src.Registered != DateTime.MinValue ? src.Registered : DateTime.UtcNow,
            IsBlocked = src.IsBlocked,
            PersonalName = src.PersonalName,
            CertAliases = src.CertAliases
        };
    }

    public static AdminUserModel ToDomain(IAdminUser src)
    {
        return new()
        {
            Id = src.Id,
            Registered = src.Registered,
            IsBlocked = src.IsBlocked,
            PersonalName = src.PersonalName.IsNotNullOrEmpty() ? src.PersonalName : src.Id,
            CertAliases = src.CertAliases,
        };
    }
}