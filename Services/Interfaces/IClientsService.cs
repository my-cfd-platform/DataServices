using AccountsManager;
using DataServices.Models.Auth.Users;
using DataServices.Models.Clients;
using Keyvalue;
using ManagerAccessFlows;
using Pd;
using ReportGrpc;
using TradeLog;

namespace DataServices.Services.Interfaces;

public interface IClientsService
{
    public Task<string> GetClientRedirectUrl(string clientId);

    Task<List<TradeLogItem>> GetTradeLog(string accountId, string traderId, DateTime from, DateTime to);

    #region Trader and Account

    Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue);
    Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue, IBackOfficeUser user);
    Task<List<TradingAccountModel>> GetTraderAccountsAsync(string traderId);
    Task<TradingAccountModel> GetTraderAccountAsync(string traderId, string accountId);
    Task<AccountsManagerOperationResult> SetTraderAccountDisabledAsync(string accountId, string traderId,
        bool disabled);

    #endregion

    #region Closed Positions

    Task<List<InvestmentPositionModel>> GetAllHistoryPositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(string accountId, DateTime from, DateTime to);

    #endregion

    #region Active Positions

    Task<List<InvestmentPositionModel>> GetAllActivePositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetActivePositionsAsync(string accountId, DateTime from, DateTime to);

    #endregion

    #region Balance

    Task<AccountManagerUpdateAccountBalanceGrpcResponse> UpdateAccountBalanceAsync(UpdateBalanceModel requestModel);
    Task<List<AccountBalanceModel>> GetBalanceHistoryAsync(string accountId);
    Task<List<ReportOperationHistoryItem>> GetOperationsHistoryAsync(string accountId, DateTime from, DateTime to);

    Task<ReportFlowsOperationsGetHistoryPaginatedGrpcResponse> GetBalanceHistoryPageAsync(string accountId,
        int page, int size = 100);

    #endregion

    #region Personal Data

    Task SetTraderPersonalData(PersonalDataModel model, string traderId);
    Task<PersonalDataModel> GetTraderPersonalDataAsync(TraderBrandModel traderBrand);

    #endregion

    #region KeyValue Service

    Task<List<GetKeyValueGrpcResponseModel>> GetClientKeyValues(string clientId);
    Task SetClientKeyValue(string clientId, string key, string value);

    #endregion

    #region Manager Access

    Task<bool> CanManagerAccess(string managerId, string traderId);
    Task SetManagerAccess(TraderAccessModel access);
    Task<List<TraderAccessModel>> GetManagerAccess(string managerId);
    Task<TraderManagers> GetTraderManagersLookup(string traderId);

    #endregion
}