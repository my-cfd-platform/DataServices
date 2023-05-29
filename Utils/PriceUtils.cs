using System.ComponentModel.DataAnnotations;
using DataServices.Models;
using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;
using Keyvalue;
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

        public static double CalcProfitLossByPrice(InvestmentPositionModel order, double nowPrice)
        {
            var opSide = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;

            order.Profit = (nowPrice / order.OpenPrice - 1) * order.InvestAmount * order.Leverage * opSide;
            return order.Profit;
        }

        public static double CalcProfitLossByPrice(InvestmentPositionModel order, BidAskModel bidAsk)
        {
            var nowPrice = GetOrderPrice(order.Side, bidAsk);
            var opSide = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;

            order.Profit = (nowPrice / order.OpenPrice - 1) * order.InvestAmount * order.Leverage * opSide;
            return order.Profit;
        }  
        
        public static void UpdateUnrealisedProfit(InvestmentPositionModel order, BidAskModel bidAsk)
        {
            var invertedInstrument = order.Quote != order.Collateral;
            var nowPrice = GetOrderPrice(order.Side, bidAsk);
            var sideCoefficient = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;

            var investmentSize = order.InvestAmount * order.Leverage;

            var investmentVolume = invertedInstrument ?
                investmentSize : 
                investmentSize / order.OpenPrice;

            var priceChange = nowPrice - order.OpenPrice;

            var instrumentProfit = investmentVolume * priceChange;

            var accountCurrencyProfit = invertedInstrument ? 
            instrumentProfit / nowPrice :
            instrumentProfit;

            order.Profit = accountCurrencyProfit * sideCoefficient;
        }

        public static ReportsFlowsPositionSide InvertSide(ReportsFlowsPositionSide side)
        {
            return side == ReportsFlowsPositionSide.Buy ? ReportsFlowsPositionSide.Sell : ReportsFlowsPositionSide.Buy;
        }

        public static void UpdateUnrealisedProfit(InvestmentPositionModel order, BidAskModel bidAsk, BidAskModel collateralBidAsk)
        {
            var invertedBaseInstrument = !order.CollateralBaseOpenBidAsk.Id.EndsWith(order.Collateral);
            var nowPrice = GetOrderPrice(order.Side, bidAsk);
            var sideCoefficient = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;
            
            var investmentSize = order.InvestAmount * order.Leverage;

            var collateralSide = invertedBaseInstrument ? order.Side : InvertSide(order.Side);
            var openPrice = GetOrderPrice(collateralSide, order.CollateralBaseOpenBidAsk);

            var investmentVolume = invertedBaseInstrument ?
                investmentSize: 
                investmentSize  / openPrice;
            
            var priceChange = nowPrice - order.OpenPrice;

            var instrumentProfit = investmentVolume * priceChange;

            var invertedQuoteInstrument = !order.CollateralQuoteCloseBidAsk.Id.EndsWith(order.Collateral);
            var quoteInstrumentSide = invertedQuoteInstrument ? InvertSide(order.Side) : order.Side;

            var quoteInstrumentPrice = GetOrderPrice(quoteInstrumentSide, collateralBidAsk);
            
            
            var accountCurrencyProfit = invertedQuoteInstrument ? 
                instrumentProfit / quoteInstrumentPrice:
                instrumentProfit ;
            order.Profit = accountCurrencyProfit * sideCoefficient;
        }

        public static double CalcProfitLossByPrice(InvestmentPositionModel order, BidAskModel bidAsk, string instrumentQuote, string accountCurrency)
        {
            //if buy get bid otherwise ask
            var currentPrice = GetOrderPrice(order.Side, bidAsk);
            
            //Profit of 1 unit in order base currency
            var orderProfitBase = (currentPrice - order.OpenPrice);

            var opSide = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;
        
            if (instrumentQuote != accountCurrency)
            {
                //convert order profit base from quote to base currency
                orderProfitBase /= currentPrice;
            }
        
            var investAmount = order.InvestAmount * order.Leverage * opSide;
            var investmentInBase = investAmount / currentPrice;
            var profit = investAmount * orderProfitBase;



            order.Profit = profit;
            return order.Profit;
        }

        public static double CalcProfitLoss(InvestmentPositionModel order, BidAskModel bidAsk, BidAskModel baseBidAsk, BidAskModel quoteBidAsk, string accountCurrency)
        {

            //if buy then bid otherwise ask
            var currentPrice = GetOrderPrice(order.Side, bidAsk);
            //Profit of 1 unit in order base currency
            var orderProfitBase = (currentPrice - order.OpenPrice);
            
            //Selling Base currency
            var baseOrderSide = baseBidAsk.Id.EndsWith(accountCurrency)
                ? ReportsFlowsPositionSide.Buy
                : ReportsFlowsPositionSide.Sell;
            var basePrice = GetOrderPrice(baseOrderSide, baseBidAsk);
            if (baseOrderSide == ReportsFlowsPositionSide.Sell)
            {
                basePrice = 1 / basePrice;
            }

            //Selling quote currency
            var quoteOrderSide = baseBidAsk.Id.EndsWith(accountCurrency)
                ? ReportsFlowsPositionSide.Buy
                : ReportsFlowsPositionSide.Sell;
            var quotePrice = GetOrderPrice(quoteOrderSide, quoteBidAsk);
            if (quoteOrderSide == ReportsFlowsPositionSide.Sell)
            {
                quotePrice = 1 / quotePrice;
            }

            var opSide = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;
            var investAmount = order.InvestAmount * order.Leverage * opSide;

            var profit = investAmount / basePrice * orderProfitBase * quotePrice;
            order.Profit = profit;//(currentPrice / order.OpenPrice - 1) * order.InvestAmount * order.Leverage * opSide;
            return order.Profit;
        }

        public static double GetOrderPrice(ReportsFlowsPositionSide op, BidAskModel bidAsk)
        {
            return op == ReportsFlowsPositionSide.Buy
                ? bidAsk.Bid
                : bidAsk.Ask;
        }

        private static double GetOrderBasePrice(InvestmentPositionModel order, BidAskModel bidAsk)
        {

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (order.CollateralBaseOpenPrice == 1)
            {
                return order.Side == ReportsFlowsPositionSide.Buy
                    ? 1/bidAsk.Bid
                    : 1/bidAsk.Ask;
            }

            return order.Side == ReportsFlowsPositionSide.Buy
                ? bidAsk.Bid
                : bidAsk.Ask;
        }
    }

