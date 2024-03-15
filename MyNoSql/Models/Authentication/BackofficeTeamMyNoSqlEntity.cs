using DataServices.Extensions;
using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces.Authentication;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models.Authentication;

public class BackofficeTeamMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeTeam
{
    public static string GeneratePartitionKey() => "bt";

    public static string GenerateRowKey(string id) => id;

    public string Id
    {
        get => RowKey;
        set => RowKey = value;
    }

    public string Name { get; set; }
    public IEnumerable<string> OfficeIds { get; set; }
    //public string TeamLeadId { get; set; }
    public TeamType Type { get; set; }
    public IEnumerable<string> Managers { get; set; }

    public static BackofficeTeamMyNoSqlEntity Create(IBackofficeTeam src)
    {
        if (src.Id.IsNullOrEmpty())
            src.Id = Guid.NewGuid().ToString();
        return new BackofficeTeamMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Name = src.Name,
            OfficeIds = src.OfficeIds,
            //TeamLeadId = src.TeamLeadId,
            Managers = src.Managers,
            Type = src.Type,
        };
    }
}