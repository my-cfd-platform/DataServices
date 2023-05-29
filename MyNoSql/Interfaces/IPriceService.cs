using DataServices.Models;
using DataServices.Models.Clients;

namespace DataServices.MyNoSql.Interfaces;

public interface IPriceService
{
    public void UpdateOrderProfit(InvestmentPositionModel order, BidAskModel currentPrice);
    public void UpdateOrderProfit(InvestmentPositionModel order);
}