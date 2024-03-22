namespace DataServices.MyNoSql.Interfaces;

public interface ITradingProfile
{
    string Id { get; }
    double MarginCallPercent { get; }
    double StopOutPercent { get; }
    double ToppingUpPercent { get; }
    bool IsABook { get; }
    IEnumerable<ITradingProfileInstrument> Instruments { get; }
}