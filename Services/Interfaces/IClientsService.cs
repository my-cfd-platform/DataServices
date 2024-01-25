using AccountsManager;
using DataServices.Models.Auth.Users;
using DataServices.Models.Clients;
using Docs;
using Keyvalue;
using Kyc;
using Kyclog;
using ManagerAccessFlows;
using Pd;
using ReportGrpc;
using System.Threading.Tasks;
using TradeLog;
using TraderFtd;
using WithdrawalsFlows;

namespace DataServices.Services.Interfaces;

public interface IClientsService
{
    public Task<string> GetClientRedirectUrl(string clientId);

    Task<List<TradeLogItem>> GetTradeLog(string accountId, string traderId, DateTime from, DateTime to);

    #region PositionManager

    Task ChargeSwap(string positionId, string accountId, double amount);

    #endregion

    #region Trader and Account

    Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue);
    Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue, IBackOfficeUser user);
    Task<List<TradingAccountModel>> GetTraderAccountsAsync(string traderId);
    Task<List<TradingAccountModel>> SearchTraderAccountsAsync(SearchAccounts search);
    Task<List<TraderBrandModel>> GetTradersByIds(IEnumerable<string> ids);
    Task<TradingAccountModel> GetTraderAccountAsync(string traderId, string accountId);
    Task<AccountsManagerOperationResult> SetTraderAccountDisabledAsync(string accountId, string traderId,
        bool disabled);
    Task UpdateTraderGroup(string accountId, string traderId, string group);

    #endregion

    #region Closed Positions

    Task<List<InvestmentPositionModel>> GetAllHistoryPositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(string accountId, string traderId, DateTime from, DateTime to);

    #endregion

    #region Active Positions

    Task<List<InvestmentPositionModel>> GetAllActivePositionsAsync(DateTime from, DateTime to);
    Task<List<InvestmentPositionModel>> GetActivePositionsAsync(string accountId, string traderId, DateTime from, DateTime to);

    #endregion

    #region Balance

    Task<AccountManagerUpdateAccountBalanceGrpcResponse> UpdateAccountBalanceAsync(UpdateBalanceModel requestModel);
    Task<List<AccountBalanceModel>> GetBalanceHistoryAsync(string accountId, string traderId);
    Task<List<ReportOperationHistoryItem>> GetOperationsHistoryAsync(string accountId, string traderId, DateTime from, DateTime to);
    Task<Dictionary<string, Dictionary<string, List<ReportOperationHistoryItem>>>> SearchOperationsHistoryAsync(SearchHistory search);
    Task<ReportFlowsOperationsGetHistoryPaginatedGrpcResponse> GetBalanceHistoryPageAsync(string accountId, string traderId, int page, int size = 100);

    #endregion

    #region Personal Data

    Task SetTraderPersonalData(PersonalDataModel model);
    Task<PersonalDataModel> GetTraderPersonalDataAsync(string traderId);

    Task<List<PersonalDataModel>> SearchPersonalDataAsync(SearchPersonal search);
    #endregion

    #region KYC

    Task SetClientKycStatusAsync(string clientId, KycStatus status, string agentId);
    Task<KycStatus> GetClientKycStatusAsync(string clientId);
    Task<List<DocumentItemModel>> GetClientDocumentsList(string clientId);
    void UpdateDocumentComment(string id, string comment, string authorId, string traderId);
    Task UpdateDocumentStatus(string id, DocumentStatus status, DocumentRejectReason reason, string authorId, string traderId);
    Task<DocumentItemModel> UploadDocument(UploadDocumentRequest request);
    Task<DocumentItemModel> UploadDocument(string authorId, string traderId, DocType docType, string content, string contentType, string comment);
    Task<DocumentModel> GetClientDocument(string clientId, string documentId);
    Task<List<ChangeLog>> GetKycChangeLogs(string traderId);
    #endregion

    #region Trader FTD

    Task<List<GetTraderFtdModel>> GetClientsFtd(List<string> traders);

    #endregion

    #region KeyValue Service

    Task<List<GetKeyValueGrpcResponseModel>> GetClientKeyValues(string clientId);
    Task<GetKeyValueGrpcResponseModel> GetClientKeyValue(string clientId, string key);
    Task<List<GetKeyValueGrpcResponseModel>> GetClientsWithKey(List<string> clients, string key);
    Task SetClientKeyValue(string clientId, string key, string value);

    #endregion

    #region Manager Access

    Task<bool> CanManagerAccessAsync(string managerId, string traderId);
    Task SetManagerAccess(TraderAccessModel access);
    Task<List<TraderAccessModel>> GetManagerAccess(string managerId);
    Task<TraderManagers> GetTraderManagersLookup(string traderId);
    Task<Dictionary<string, TraderManagers>> SearchTraderManagersAsync(SearchManager search);

    #endregion

    #region Withdrawals

    Task<List<WithdrawalGrpcModel>> GetActiveWithdrawalRequestsAsync(string? traderId = null);
    Task ApproveWithdrawalRequestsAsync(string id, string traderId, string accountId, string agentId);
    Task DenyWithdrawalRequestsAsync(string id, string traderId, string accountId, string agentId);

    #endregion
}