using DataServices.Extensions;
using DataServices.MyNoSql.Interfaces;
using ReportGrpc;

namespace DataServices.Models;

public class BidAskModel
{
    public string Id { get; set; }

    public string Date { get; set; }

    public double Bid { get; set; }

    public double Ask { get; set; }

    public bool TimeWarning { get; set; }
}


public class UnfilteredBidAskModel
{
    public string Id { get; set; }

    public string Date { get; set; }

    public double Bid { get; set; }

    public double Ask { get; set; }

    public string Provider { get; set; }

    public bool TimeWarning { get; set; }
}

public static class PriceModels
{

    public static BidAskModel ToBidAskModel(this ILivePrice livePrice)
    {
        var date = livePrice.UnixTimestampWithMilis.EpochMilToDateTime();
        return new BidAskModel
        {
            Id = livePrice.Id,
            Date = date.ToString("yyyy-MM-dd HH:mm:ss"),
            Bid = livePrice.Bid,
            Ask = livePrice.Ask,
            TimeWarning = DateTime.Now - date > TimeSpan.FromMinutes(3),
        };
    }
        
    public static BidAskModel ToBidAskModel(this ReportsFlowsBidAsk price)
    {
        var date = price.DateTimeUnixTimestampMilis.EpochMicToDateTime();
        return new BidAskModel
        {
            Id = price.AssetPair,
            Date = date.ToString("yyyy-MM-dd HH:mm:ss"),
            Bid = price.Bid,
            Ask = price.Ask,
            TimeWarning = DateTime.Now - date > TimeSpan.FromMinutes(3),
        };
    }
}