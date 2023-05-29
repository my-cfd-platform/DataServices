using DataServices.Extensions;
using ReportGrpc;

namespace DataServices.Models.Clients;

public class InvestmentPositionModel
{
    public string Id { get; set; }
    public PositionStatus Status { get; set; }
    public string AccountId { get; set; }
    public string TraderId { get; set; }
    public string Instrument { get; set; }
    public ReportsFlowsPositionSide Side { get; set; }
    public double InvestAmount { get; set; }
    public double Leverage { get; set; }
    public double StopOutPercent { get; set; }
    public string CreateProcessId { get; set; }
    public DateTime Created { get; set; }
    public string LastUpdateProcessId { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public double TpInProfit { get; set; }
    public double SlInProfit { get; set; }
    public double TakeProfitInAssetPrice { get; set; }
    public double StopLossInAssetPrice { get; set; }
    public double OpenPrice { get; set; }
    public BidAskModel OpenBidAsk { get; set; }
    public string OpenProcessId { get; set; }
    public DateTime OpenDate { get; set; }
    public double ClosePrice { get; set; }
    public BidAskModel CloseBidAsk { get; set; }
    public string CloseProcessId { get; set; }
    public DateTime CloseDate { get; set; }
    public ReportsFlowsClosePositionReason CloseReason { get; set; }
    public double Profit { get; set; }

    public string Base { get; set; }
    public string Quote { get; set; }
    public string Collateral { get; set; }
    public double CollateralBaseOpenPrice { get; set; }
    public double CollateralQuoteOpenPrice { get; set; }
    public BidAskModel CollateralBaseOpenBidAsk { get; set; }
    public BidAskModel CollateralQuoteCloseBidAsk { get; set; }

    public static InvestmentPositionModel FromGrpc(ReportsFlowsActivePositionGrpcModel src)
    {
        return new InvestmentPositionModel
        {
            Id = src.Id,
            Status = PositionStatus.Open,
            AccountId = src.AccountId,
            TraderId = src.TraderId,
            Side = src.Side,
            Instrument = src.AssetPair,
            Created = src.CreateDateUnixTimestampMilis.EpochMicToDateTime(),
            CreateProcessId = src.CreateProcessId,
            InvestAmount = src.InvestAmount,
            LastUpdateDate = src.LastUpdateDate.EpochMicToDateTime(),
            LastUpdateProcessId = src.LastUpdateProcessId,
            Leverage = src.Leverage,
            OpenBidAsk = src.OpenBidAsk.ToBidAskModel(),
            OpenProcessId = src.OpenProcessId,
            OpenDate = src.OpenDate.EpochMicToDateTime(),
            Base = src.Base,
            Quote = src.Quote,
            Collateral = src.Collateral,
            CollateralBaseOpenPrice = src.CollateralBaseOpenPrice,
            CollateralQuoteOpenPrice = src.CollateralQuoteClosePrice,
            CollateralBaseOpenBidAsk = src.CollateralBaseOpenBidAsk?.ToBidAskModel()!,
            CollateralQuoteCloseBidAsk = src.CollateralQuoteCloseBidAsk?.ToBidAskModel()!,
            OpenPrice = src.OpenPrice,
            TpInProfit = src.TpInProfit,
            SlInProfit = src.SlInProfit,
            TakeProfitInAssetPrice = src.TpInAssetPrice,
            StopLossInAssetPrice = src.SlInAssetPrice,
            StopOutPercent = src.StopOutPercent
        };
    }

    public static InvestmentPositionModel FromGrpc(ReportsFlowsClosedPositionGrpcModel src)
    {
        return new InvestmentPositionModel
        {
            Id = src.Id,
            Status = PositionStatus.Closed,
            AccountId = src.AccountId,
            TraderId = src.TraderId,
            Side = src.Side,
            Instrument = src.AssetPair,
            Created = src.CreateDateUnixTimestampMilis.EpochMicToDateTime(),
            CreateProcessId = src.CreateProcessId,
            InvestAmount = src.InvestAmount,
            LastUpdateDate = src.LastUpdateDate.EpochMicToDateTime(),
            LastUpdateProcessId = src.LastUpdateProcessId,
            Leverage = src.Leverage,
            OpenBidAsk = src.OpenBidAsk.ToBidAskModel(),
            OpenProcessId = src.OpenProcessId,
            OpenDate = src.OpenDate.EpochMicToDateTime(),
            OpenPrice = src.OpenPrice,
            CloseBidAsk = src.CloseBidAsk.ToBidAskModel(),
            CloseDate = src.CloseDate.EpochMicToDateTime(),
            ClosePrice = src.ClosePrice,
            CloseProcessId = src.CloseProcessId,
            CloseReason = src.CloseReason,
            Profit = src.Profit,
            TpInProfit = src.TpInProfit,
            SlInProfit = src.SlInProfit,
            TakeProfitInAssetPrice = src.TpInAssetPrice,
            StopLossInAssetPrice = src.SlInAssetPrice,
            StopOutPercent = src.StopOutPercent,
            Base = src.Base,
            Quote = src.Quote,
            Collateral = src.Collateral,
            CollateralBaseOpenPrice = src.CollateralBaseOpenPrice,
            CollateralQuoteOpenPrice = src.CollateralQuoteClosePrice,
            CollateralBaseOpenBidAsk = src.CollateralBaseOpenBidAsk?.ToBidAskModel()!,
            CollateralQuoteCloseBidAsk = src.CollateralQuoteCloseBidAsk?.ToBidAskModel()!,
        };
    }
}

