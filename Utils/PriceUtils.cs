using DataServices.Models;
using ReportGrpc;

namespace DataServices.Utils;
public static class PriceUtils
{
    public static int CalcSpread(BidAskModel bidAsk, int instrumentDigits)
    {
        var multiplier = Math.Pow(10, instrumentDigits);
        return (int)(bidAsk.Ask * multiplier - bidAsk.Bid * multiplier);
    }

    public static int CalcSpread(UnfilteredBidAskModel bidAsk, int instrumentDigits)
    {
        var multiplier = Math.Pow(10, instrumentDigits);
        return (int)(bidAsk.Ask * multiplier - bidAsk.Bid * multiplier);
    }

    public static int CalcTicks(double priceBefore, double priceAfter, int instrumentDigits)
    {
        var multiplier = Math.Pow(10, instrumentDigits);
        return (int)(priceAfter * multiplier - priceBefore * multiplier);
    }

    public static ReportsFlowsPositionSide InvertSide(ReportsFlowsPositionSide side)
    {
        return side == ReportsFlowsPositionSide.Buy ? ReportsFlowsPositionSide.Sell : ReportsFlowsPositionSide.Buy;
    }

    public static double GetOrderPrice(ReportsFlowsPositionSide op, BidAskModel bidAsk)
    {
        return op == ReportsFlowsPositionSide.Buy
            ? bidAsk.Bid
            : bidAsk.Ask;
    }

}

