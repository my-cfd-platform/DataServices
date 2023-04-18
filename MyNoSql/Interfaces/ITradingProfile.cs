namespace DataServices.MyNoSql.Interfaces;

public interface ITradingProfile
{
    string Id { get; }
    double MarginCallPercent { get; }
    double StopOutPercent { get; }
    double PositionToppingUpPercent { get; }
    bool IsABook { get; }
    IEnumerable<ITradingProfileInstrument> Instruments { get; }
}