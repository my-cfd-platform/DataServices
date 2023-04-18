using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class ProviderInstrumentEntity : MyNoSqlDbEntity, IProviderInstrumentMap
{
    public string Id => RowKey;

    public string LpId { get; set; }
    public IDictionary<string, string> Map { get; set; }


    public static string GeneratePartitionKey()
    {
        return "im";
    }

    public static string GenerateRowKey(string id)
    {
        return id;
    }
    public static ProviderInstrumentEntity Create(IProviderInstrumentMap src)
    {
        return new ProviderInstrumentEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.LpId),
            LpId = src.LpId,
            Map = src.Map
        };
    }
}