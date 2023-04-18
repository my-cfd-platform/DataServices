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

    public double PositionToppingUpPercent { get; set; }

    public bool IsABook { get; set; }

    IEnumerable<ITradingProfileInstrument> ITradingProfile.Instruments =>
        Instruments;

    public List<TradingProfileInstrumentMyNoSqlEntity> Instruments { get; set; }

    public static TradingProfileMyNoSqlEntity Create(ITradingProfile src)
    {
        TradingProfileMyNoSqlEntity profileMyNoSqlEntity = new TradingProfileMyNoSqlEntity();
        profileMyNoSqlEntity.PartitionKey = TradingProfileMyNoSqlEntity.GeneratePartitionKey();
        profileMyNoSqlEntity.RowKey = TradingProfileMyNoSqlEntity.GenerateRowKey(src.Id);
        profileMyNoSqlEntity.MarginCallPercent = src.MarginCallPercent;
        profileMyNoSqlEntity.PositionToppingUpPercent = src.PositionToppingUpPercent;
        profileMyNoSqlEntity.StopOutPercent = src.StopOutPercent;
        profileMyNoSqlEntity.Instruments = src.Instruments.Select<ITradingProfileInstrument, TradingProfileInstrumentMyNoSqlEntity>(new Func<ITradingProfileInstrument, TradingProfileInstrumentMyNoSqlEntity>(TradingProfileInstrumentMyNoSqlEntity.Create)).ToList<TradingProfileInstrumentMyNoSqlEntity>();
        profileMyNoSqlEntity.IsABook = src.IsABook;
        return profileMyNoSqlEntity;
    }
}