using DataServices.Models;
using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;
using DataServices.Utils;
using ReportGrpc;

namespace DataServices.Services;

public class PriceService : IPriceService
{
    private readonly IInstrumentCache _instrumentsCache;
    private readonly ICache<ILivePrice> _priceCache;

    public PriceService(IInstrumentCache instrumentsCache, ICache<ILivePrice> priceCache)
    {
        _instrumentsCache = instrumentsCache;
        _priceCache = priceCache;
    }

    public void UpdateOrderProfit(InvestmentPositionModel order)
    {
        var instrumentCurrentTick = _priceCache.Get(order.Instrument).ToBidAskModel();
        UpdateOrderProfit(order, instrumentCurrentTick);
    }

    public void UpdateOrderProfit(InvestmentPositionModel order, BidAskModel currentPrice)
    {
        var accountCurrency = order.Collateral;

        var simpleInstrument = order.Base == accountCurrency || order.Quote == accountCurrency;
        if (simpleInstrument)
        {
            UpdateUnrealisedProfit(order, currentPrice);
        }
        else
        {
            UpdateUnrealisedCollateralProfit(order, currentPrice);
        }
    }

    private void UpdateUnrealisedProfit(InvestmentPositionModel order, BidAskModel bidAsk)
    {
        var accountCurrency = order.Collateral;

        var invertedInstrument = order.Quote != accountCurrency;
        var nowPrice = PriceUtils.GetOrderPrice(order.Side, bidAsk);
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
        var orderSwaps = order.Swaps.Sum(a => a.Amount);
        order.Profit = accountCurrencyProfit * sideCoefficient - orderSwaps;
    }

    // order = order to update, bidAsk = current order instrument price
    private void UpdateUnrealisedCollateralProfit(InvestmentPositionModel order, BidAskModel bidAsk)
    {
        var accountCurrency = order.Collateral;

        #region Profit in Base currency

        var baseInstrument = _instrumentsCache.Get(order.CollateralBaseOpenBidAsk.Id);
        var invertedBaseInstrument = baseInstrument.Quote != accountCurrency;

        var investmentSize = order.InvestAmount * order.Leverage;

        // the side of collateral base open price must be the same as the order side
        var collateralSide = order.Side;


        var collateralOpenPrice = PriceUtils.GetOrderPrice(collateralSide, order.CollateralBaseOpenBidAsk);

        var investmentBaseSize = invertedBaseInstrument ?
            investmentSize * collateralOpenPrice :
            investmentSize / collateralOpenPrice;


        var currentPrice = PriceUtils.GetOrderPrice(order.Side, bidAsk);
        var priceChange = currentPrice - order.OpenPrice;

        var baseInstrumentProfit = investmentBaseSize * priceChange;

        #endregion

        #region Profit in account currency

        var quoteInstrument = _instrumentsCache.FindByCurrency(order.Base, order.Quote);
        var invertedQuoteInstrument = quoteInstrument!.Quote != accountCurrency;

        // the side of collateral quote close/current price must be the same as the order side
        var quoteInstrumentSide = PriceUtils.InvertSide(order.Side);

        var collateralQuoteBidAsk = _priceCache.Get(quoteInstrument.Id).ToBidAskModel();

        var quoteInstrumentPrice = PriceUtils.GetOrderPrice(quoteInstrumentSide, collateralQuoteBidAsk);

        var accountCurrencyProfit = invertedQuoteInstrument ?
            baseInstrumentProfit / quoteInstrumentPrice :
            baseInstrumentProfit * quoteInstrumentPrice;

        #endregion

        var sideCoefficient = order.Side == ReportsFlowsPositionSide.Buy ? 1 : -1;
        var orderSwaps = order.Swaps.Sum(a => a.Amount);
        order.Profit = accountCurrencyProfit * sideCoefficient - orderSwaps;
    }
}