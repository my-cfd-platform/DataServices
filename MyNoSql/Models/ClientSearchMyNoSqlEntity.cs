using DataServices.Models.Clients.Search;
using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class ClientSearchMyNoSqlEntity : MyNoSqlDbEntity, IClientSearch
{

    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
    // Creates random guid for Client Search records
    private static string GenerateRowKey(IClientSearch src) => src.Id ?? Guid.NewGuid().ToString();
    public static string GeneratePartitionKey() => "cs";

    public string Id => RowKey;
    public string Name { get; set; }
    public ClientSearchEntity Data { get; set; }
    //public Dictionary<string,string> Data { get; set; }
    public static ClientSearchMyNoSqlEntity Create(IClientSearch src)
    {
        return new ClientSearchMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src),
            Data = src.Data,
            Name = src.Name,
        };
    }
}