using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Mappers;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models;

public class SwapScheduleMyNoSqlEntity : MyNoSqlDbEntity, ISwapSchedule
{
    public static string GeneratePartitionKey(ISwapSchedule src)
    {
        return src.SwapProfileId;
    }

    public static string GenerateRowKey(ISwapSchedule src)
    {
        return src.Id;
    }
    
    public string Id { get; set; }
    public string SwapProfileId => PartitionKey;
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan Time { get; set; }
    public int Amount { get; set; }

    public static SwapScheduleMyNoSqlEntity Create(ISwapSchedule src)
    {
        return new SwapScheduleMyNoSqlEntity
        {
            Id = src.Id,
            PartitionKey = GeneratePartitionKey(src),
            DayOfWeek = src.DayOfWeek,
            RowKey = GenerateRowKey(src),
            Amount = src.Amount,
            Time = src.Time
        };
    }
}