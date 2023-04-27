using DataServices.Models;
using DataServices.Models.Clients;
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
        
        public static double GetOrderPrice(ReportsFlowsPositionSide op, BidAskModel bidAsk)
        {
            return op == ReportsFlowsPositionSide.Buy
                ? bidAsk.Bid
                : bidAsk.Ask;
        }

        public static double CalcProfitLossByPrice(InvestmentPositionModel order, double nowPrice)
        {
            var opSide = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;

            order.Profit = (nowPrice / order.OpenPrice - 1) * order.InvestAmount * order.Leverage * opSide;
            return order.Profit;
        }
    }

