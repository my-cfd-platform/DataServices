namespace DataServices.MyNoSql.Interfaces;

public interface ITradingInstrument
{
#nullable disable
    string Id { get; }
    string Name { get; }
    int Digits { get; }
    string Base { get; }
    string Quote { get; }
    double TickSize { get; }
    string SwapScheduleId { get; }

#nullable enable
    string? GroupId { get; set; }
    string? SubGroupId { get; set; }
    int? Weight { get; set; }

#nullable disable
    IEnumerable<ITradingInstrumentDayOff> DaysOff { get; }
    int? DayTimeout { get; set; }
    int? NightTimeout { get; set; }
    int? MarginCallPercent { get; set; }
    bool TradingDisabled { get; set; }
}