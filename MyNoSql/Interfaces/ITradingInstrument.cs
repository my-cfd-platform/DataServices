namespace DataServices.MyNoSql.Interfaces;

public interface ITradingInstrument
{
#nullable disable
    string Id { get; }
    string Name { get; set; }
    int Digits { get; set; }
    string Base { get; set; }
    string Quote { get; set; }
    double TickSize { get; set; }
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