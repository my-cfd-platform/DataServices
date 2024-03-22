using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class TradingProfileMyNoSqlEntity : MyNoSqlDbEntity, ITradingProfile
{
    public static string GeneratePartitionKey() => "profile";

    public static string GenerateRowKey(string id) => id;

    public string Id => this.RowKey;

    public double MarginCallPercent { get; set; }

    public double StopOutPercent { get; set; }

    public double ToppingUpPercent { get; set; }

    public bool IsABook { get; set; }

    IEnumerable<ITradingProfileInstrument> ITradingProfile.Instruments => Instruments;

    public List<TradingProfileInstrumentMyNoSqlEntity> Instruments { get; set; }

    public static TradingProfileMyNoSqlEntity Create(ITradingProfile src)
    {

        return new TradingProfileMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            MarginCallPercent = src.MarginCallPercent,
            ToppingUpPercent = src.ToppingUpPercent,
            StopOutPercent = src.StopOutPercent,
            Instruments = src.Instruments.Select(TradingProfileInstrumentMyNoSqlEntity.Create).ToList(),
            IsABook = src.IsABook
        };
    }
}