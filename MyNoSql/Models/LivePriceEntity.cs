using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class LivePriceEntity : MyNoSqlDbEntity, ILivePrice
{
    public string Id => RowKey;

    public long UnixTimestampWithMilis { get; set; }
    public double Bid { get; set; }
    public double Ask { get; set; }


    public static string GeneratePartitionKey()
    {
        return "instruments_snapshot";
    }

    public static string GenerateRowKey(string id)
    {
        return id;
    }
    public static LivePriceEntity Create(ILivePrice src)
    {
        return new LivePriceEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            UnixTimestampWithMilis = src.UnixTimestampWithMilis,
            Bid = src.Bid,
            Ask = src.Ask
        };
    }
}