using DataServices.Extensions;
using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class BackofficeTeamMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeTeam
{
    public static string GeneratePartitionKey() => "bt";

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
}