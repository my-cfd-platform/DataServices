namespace DataServices.MyNoSql.Interfaces;

public interface ISwapSchedule
{
    string Id { get; }
    public string SwapProfileId { get; }
    DayOfWeek DayOfWeek { get; }
    TimeSpan Time { get; }
    int Amount { get; }
}