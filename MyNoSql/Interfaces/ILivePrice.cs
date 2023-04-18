namespace DataServices.MyNoSql.Interfaces;

public interface ILivePrice
{
    public string Id { get; }
    public long UnixTimestampWithMilis { get; }
    public double Bid { get; set; }
    public double Ask { get; set; }
}