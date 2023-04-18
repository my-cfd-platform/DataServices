using AccountsManager;
using DataServices.Models.Clients;
using ReportGrpc;
using TradeLog;

namespace DataServices.Services.Interfaces;

public interface IClientsService
{
    Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue);
    Task<List<TradingAccountModel>> GetTraderAccountsAsync(string traderId);
    public Task<string> GetClientRedirectUrl(string clientId);

    Task<AccountsManagerOperationResult> SetTraderAccountDisabledAsync(string accountId, string traderId,
        bool disabled);

    Task<AccountManagerUpdateAccountBalanceGrpcResponse> UpdateAccountBalanceAsync(UpdateBalanceModel requestModel);
    Task<List<AccountBalanceModel>> GetBalanceHistoryAsync(string accountId);

    Task<ReportFlowsOperationsGetHistoryPaginatedGrpcResponse> GetBalanceHistoryPageAsync(string accountId,
        int page, int size = 100);

    Task<List<InvestmentPositionModel>> GetAllActivePositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetActivePositionsAsync(string accountId, DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetAllHistoryPositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(string accountId, DateTime from, DateTime to);
    Task<List<TradeLogItem>> GetTradeLog(string accountId, string traderId, DateTime from, DateTime to);
}