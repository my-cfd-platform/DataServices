using DataServices.MyNoSql.Interfaces;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class TradingInstrumentMyNoSqlEntity : MyNoSqlDbEntity, ITradingInstrument
{
    public static string GeneratePartitionKey()
    {
        return "i";
    }

    public static string GenerateRowKey(string id)
    {
        return id;
    }

    public string Id => RowKey;
        
    public string Name { get; set; }
        
    public int Digits { get; set; }
        
    public string Base { get; set; }
        
    public string Quote { get; set; }
        
    public double TickSize { get; set; }
        
    public string SwapScheduleId { get; set; }
        
    public string? GroupId { get; set; }
        
    public string? SubGroupId { get; set; }

    public int? Weight { get; set; }

    IEnumerable<ITradingInstrumentDayOff> ITradingInstrument.DaysOff => DaysOff;

    // TODO Remove default values
    public int? DayTimeout { get; set; } = 30;

    public int? NightTimeout { get; set; } = 30;
    public int? MarginCallPercent { get; set; } = 0;

    public bool TradingDisabled { get; set; }

    public List<TradingInstrumentDayOffMyNoSqlEntity> DaysOff { get; set; }
        
    public static TradingInstrumentMyNoSqlEntity Create(ITradingInstrument src)
    {
        return new TradingInstrumentMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            Name = src.Name,
            Digits = src.Digits,
            Base = src.Base,
            Quote = src.Quote,
            TickSize = src.TickSize,
            SwapScheduleId = src.SwapScheduleId,
            GroupId = src.GroupId,
            SubGroupId = src.SubGroupId,
            Weight = src.Weight,
            DayTimeout = src.DayTimeout,
            NightTimeout = src.NightTimeout,
            TradingDisabled = src.TradingDisabled,
            DaysOff = src.DaysOff == null 
                ? new List<TradingInstrumentDayOffMyNoSqlEntity>() :
                src.DaysOff.Select(TradingInstrumentDayOffMyNoSqlEntity.Create).ToList()
        };
    }
}