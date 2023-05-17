using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class StatusMyNoSqlEntity : MyNoSqlDbEntity, IStatus
{

    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
    // Creates random guid for custom client statuses
    private static string GenerateRowKey(IStatus src) => src.Id?? Guid.NewGuid().ToString();

    public string Id
    {
        get => RowKey;
        init => RowKey = value;
    }

    public string Name { get; set; }

    public string Type
    {
        get => PartitionKey;
        set => PartitionKey = value;
    }

    public IEnumerable<string> Labels { get; set; } = new List<string>();

    public static StatusMyNoSqlEntity Create(IStatus src)
    {
        return new StatusMyNoSqlEntity
        {
            PartitionKey = src.Type,
            RowKey = GenerateRowKey(src),
            Name = src.Name,
            Labels = src.Labels
        };
    }
}