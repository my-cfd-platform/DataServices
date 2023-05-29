namespace DataServices.MyNoSql.Interfaces;

public interface ITradingInstrumentDayOff
{
    DayOfWeek DowFrom { get; }
    TimeSpan TimeFrom { get; }
    DayOfWeek DowTo { get; }
    TimeSpan TimeTo { get; }
}