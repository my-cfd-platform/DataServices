using System.Text;
using AccountsManager;
using AuthenticationIntegration;
using DataServices.Extensions;
using DataServices.Models;
using DataServices.Models.Auth.Users;
using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;
using DataServices.Services.Interfaces;
using DataServices.Utils;
using Docs;
using DotNetCoreDecorators;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using Keyvalue;
using ManagerAccessFlows;
using Pd;
using ReportGrpc;
using TradeLog;
using TraderCredentials;
using Kyc;
using Kyclog;
using PositionManager;
using TraderFtd;
using WithdrawalsFlows;
using GetTradersRequest = TraderCredentials.GetTradersRequest;

namespace DataServices.Services;

public class ClientsService : IClientsService
{
    private readonly TraderCredentialsGrpcService.TraderCredentialsGrpcServiceClient? _traderCredentialsClient;
    private readonly ReportGrpcService.ReportGrpcServiceClient? _reportClient;
    private readonly AuthenticationGrpcService.AuthenticationGrpcServiceClient? _authServiceClient;
    private readonly PersonalDataService.PersonalDataServiceClient? _personalDataClient;
    private readonly AccountsManagerGrpcService.AccountsManagerGrpcServiceClient? _accountsManagerClient;
    private readonly TradeLogGrpcService.TradeLogGrpcServiceClient? _tradeLogClient;
    private readonly KeyValueFlowsGrpcService.KeyValueFlowsGrpcServiceClient? _keyValueClient;
    private readonly ManagerAccessService.ManagerAccessServiceClient? _managerAccessClient;
    private readonly KycStatusService.KycStatusServiceClient? _kycStatusClient;
    private readonly DocumentsService.DocumentsServiceClient? _documentsClient;
    private readonly KycChangeLogsService.KycChangeLogsServiceClient? _kycChangeLogsClient;
    private readonly WithdrawalFlowsService.WithdrawalFlowsServiceClient? _withdrawalsClient;
    private readonly PositionManagerGrpcService.PositionManagerGrpcServiceClient? _positionManagerClient;
    private readonly TraderFtdGrpcService.TraderFtdGrpcServiceClient? _traderFtdClient;

    private readonly IPriceService _priceService;

    public ClientsService(DataServicesSettings settings, IPriceService priceService)
    {
        _priceService = priceService;
        if (settings.KeyValueGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _keyValueClient = new KeyValueFlowsGrpcService.KeyValueFlowsGrpcServiceClient(
                GrpcChannel.ForAddress(settings.KeyValueGrpcServiceUrl));
        }

        if (settings.PersonalDataGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _personalDataClient = new PersonalDataService.PersonalDataServiceClient(
                GrpcChannel.ForAddress(settings.PersonalDataGrpcServiceUrl));
        }

        if (settings.TraderCredentialsFlowsGrpcUrl.IsNotNullOrEmpty())
        {
            _traderCredentialsClient = new TraderCredentialsGrpcService.TraderCredentialsGrpcServiceClient(
                GrpcChannel.ForAddress(settings.TraderCredentialsFlowsGrpcUrl));
        }

        if (settings.ReportGrpcUrl.IsNotNullOrEmpty())
        {
            _reportClient = new ReportGrpcService.ReportGrpcServiceClient(
                GrpcChannel.ForAddress(settings.ReportGrpcUrl));
        }

        if (settings.AuthGrpcUrl.IsNotNullOrEmpty())
        {
            _authServiceClient = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(
                GrpcChannel.ForAddress(settings.AuthGrpcUrl));
        }

        if (settings.TradeLogGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _tradeLogClient = new TradeLogGrpcService.TradeLogGrpcServiceClient(
                GrpcChannel.ForAddress(settings.TradeLogGrpcServiceUrl));
        }

        if (settings.AccountsManagerGrpcUrl.IsNotNullOrEmpty())
        {
            _accountsManagerClient = new AccountsManagerGrpcService.AccountsManagerGrpcServiceClient(
                GrpcChannel.ForAddress(settings.AccountsManagerGrpcUrl));
        }

        if (settings.ManagerAccessGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _managerAccessClient = new ManagerAccessService.ManagerAccessServiceClient(
                GrpcChannel.ForAddress(settings.ManagerAccessGrpcServiceUrl));
        }
        
        if (settings.KycStatusGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _kycStatusClient = new KycStatusService.KycStatusServiceClient(
                GrpcChannel.ForAddress(settings.KycStatusGrpcServiceUrl));
        }

        if (settings.DocumentsGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _documentsClient = new DocumentsService.DocumentsServiceClient(
                GrpcChannel.ForAddress(settings.DocumentsGrpcServiceUrl));
        }

        if (settings.KycChangeLogsGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _kycChangeLogsClient = new KycChangeLogsService.KycChangeLogsServiceClient(
                GrpcChannel.ForAddress(settings.KycChangeLogsGrpcServiceUrl));
        }

        if (settings.WithdrawalsGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _withdrawalsClient = new WithdrawalFlowsService.WithdrawalFlowsServiceClient(
                GrpcChannel.ForAddress(settings.WithdrawalsGrpcServiceUrl));
        }
        
        if (settings.PositionsManagerServiceUrl.IsNotNullOrEmpty())
        {
            _positionManagerClient = new(
                GrpcChannel.ForAddress(settings.PositionsManagerServiceUrl));
        }
        if (settings.TraderFtdFlowsGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _traderFtdClient = new(
                GrpcChannel.ForAddress(settings.TraderFtdFlowsGrpcServiceUrl));
        }
    }

    #region TraderFtd

    public async Task<List<GetTraderFtdModel>> GetClientsFtd(List<string> traders)
    {
        var result = new List<GetTraderFtdModel>();
        var call = _traderFtdClient!.Get();
        foreach (var traderId in traders)
        {
            await call.RequestStream.WriteAsync(new ()
            {
                TraderId = traderId

            });
        }
        await call.RequestStream.CompleteAsync();

        while (await call.ResponseStream.MoveNext())
        {
            var ftdModel = call.ResponseStream.Current;

            result.Add(ftdModel);
        }

        return result;

    }
    

    #endregion

    #region Position Manager

    public async Task ChargeSwap(string positionId, string accountId, double amount)
    {
        await _positionManagerClient!.ChargeSwapAsync(new ()
        {
            PositionId = positionId,
            AccountId = accountId,
            SwapAmount = amount
        });
    }

    #endregion

    #region Trader and Account

    public async Task<List<TraderBrandModel>> GetTradersByIds(IEnumerable<string> ids)
    {
        var request = new GetTradersRequest();
        request.TraderIds.AddRange(ids);
        var stream = _traderCredentialsClient!.GetTraders(request).ResponseStream;

        var data = new List<TraderBrandModel>();
       while (await stream.MoveNext())
       {
           data.Add(TraderBrandModel.FromGrpc(stream.Current));
       }
       return data;
    }

    public async Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue)
    {
        var traderIds = await _traderCredentialsClient!.SearchByIdOrEmailAsync(new()
        {
            Phrase = searchValue
        });

        if (traderIds.CalculateSize() == 0)
        {
            var traderIdByAccountIdRes = await _accountsManagerClient!.GetTraderIdByAccountIdAsync(new AccountManagerGetTraderIdByAccountIdGrpcRequest()
            {
                AccountId = searchValue
            });
            if (traderIdByAccountIdRes.HasTraderId)
            {
                traderIds = await _traderCredentialsClient!.SearchByIdOrEmailAsync(new()
                {
                    Phrase = traderIdByAccountIdRes.TraderId
                });
            }
        }

        return traderIds.Ids.Count == 0 ?
            new() :
            traderIds.Ids.Select(TraderBrandModel.FromGrpc).ToList();
    }

    public async Task<List<TraderBrandModel>> SearchTraderBrandsAsync(string searchValue, IBackOfficeUser user)
    {
        var allTraderBrands = await SearchTraderBrandsAsync(searchValue);
        if (user is { IsAdmin: true, IsBlocked: false })
        {
            return allTraderBrands;
        }
        return (from model in allTraderBrands
                where CanManagerAccess(user, model.TraderId)
                select model).ToList();
    }

    private async Task<List<TradingAccountModel>> ProcessAccountsStream(IAsyncStreamReader<AccountGrpcModel> stream)
    {
        var accounts = new List<TradingAccountModel>();
        while (await stream.MoveNext())
        {
            accounts.Add(TradingAccountModel.FromGrpc(stream.Current));
        }
        return accounts;
    }

    public async Task<List<TradingAccountModel>> SearchTraderAccountsAsync(SearchAccounts search)
    {
        var traderAccountsStream = _accountsManagerClient!.Search(search).ResponseStream;

        return await ProcessAccountsStream(traderAccountsStream);
    }

    public async Task<List<TradingAccountModel>> GetTraderAccountsAsync(string traderId)
    {
        var traderAccountsStream =
            _accountsManagerClient!
                .GetClientAccounts(new() { TraderId = traderId })
                .ResponseStream;

        return await ProcessAccountsStream(traderAccountsStream);
    }

    public async Task<TradingAccountModel> GetTraderAccountAsync(string traderId, string accountId)
    {
        var traderAccountsStream =
            _accountsManagerClient!
                .GetClientAccounts(new() { TraderId = traderId })
                .ResponseStream;

        while (await traderAccountsStream.MoveNext())
        {
            if (traderAccountsStream.Current.Id == accountId)
            {
                return TradingAccountModel.FromGrpc(traderAccountsStream.Current);
            }
        }
        return default!;
    }

    public async Task<AccountsManagerOperationResult> SetTraderAccountDisabledAsync(string accountId, string traderId, bool disabled)
    {
        var res = await _accountsManagerClient!.UpdateAccountTradingDisabledAsync(new()
        {
            AccountId = accountId,
            ProcessId = GetProcessId(),
            TraderId = traderId,
            TradingDisabled = disabled
        });
        return res.Result;
    }

    public async Task UpdateTraderGroup(string accountId, string traderId, string group)
    {
        await _accountsManagerClient!.UpdateAccountTradingGroupAsync(new AccountManagerUpdateTradingGroupGrpcRequest
        {
            TraderId = traderId,
            AccountId = accountId,
            NewTradingGroup = group,
            ProcessId = GetProcessId()
        });
    }

    #endregion

    #region KeyValue Service

    public async Task<List<GetKeyValueGrpcResponseModel>> GetClientKeyValues(string clientId)
    {
        var keyValuesList = new List<GetKeyValueGrpcResponseModel>();
        var keyValuesStream = _keyValueClient!
            .GetAllByUser(new()
            {
                ClientId = clientId
            })
            .ResponseStream;
        while (await keyValuesStream.MoveNext())
        {
            keyValuesList.Add(keyValuesStream.Current);
        }

        return keyValuesList;
    }

    public async Task<List<GetKeyValueGrpcResponseModel>> GetClientsWithKey(List<string> clients, string key)
    {
        var result = new List<GetKeyValueGrpcResponseModel>();
        var call = _keyValueClient!.Get();
        foreach (var clientId in clients)
        {
            await call.RequestStream.WriteAsync(new GetKeyValueGrpcRequestModel
            {
                ClientId = clientId,
                Key = key

            });
        }
        await call.RequestStream.CompleteAsync();

        while (await call.ResponseStream.MoveNext())
        {
            var keyValue = call.ResponseStream.Current;
            if(!keyValue.HasValue)
                continue;
            result.Add(keyValue);
        }

        return result;
    }

    public async Task<GetKeyValueGrpcResponseModel> GetClientKeyValue(string clientId, string key)
    {
        var call = _keyValueClient!.Get();
        
        await call.RequestStream.WriteAsync(new GetKeyValueGrpcRequestModel
        {
            ClientId = clientId,
            Key = key

        });
        await call.RequestStream.CompleteAsync();
        var stream = call.ResponseStream;

        await stream.MoveNext();

        return stream.Current;
    }

    public async Task SetClientKeyValue(string clientId, string key, string value)
    {
        var call = _keyValueClient.Set();
        await call.RequestStream.WriteAsync(new SetKeyValueGrpcRequestModel
        {
            ClientId = clientId,
            Key = key,
            Value = value,

        });
        await call.RequestStream.CompleteAsync();
    }

    #endregion

    #region Manager Access
    
    private bool CanManagerAccess(IBackOfficeUser manager, string traderId)
    {
        var res = _managerAccessClient!.CanAccess(new() { ManagerId = manager.Id, TraderId = traderId });
        return res.Value;
    }


    public async Task<bool> CanManagerAccessAsync(string managerId, string traderId)
    {
        var res = await _managerAccessClient!.CanAccessAsync(new() { ManagerId = managerId, TraderId = traderId });
        return res.Value;
    }

    public async Task SetManagerAccess(TraderAccessModel access)
    {
        await _managerAccessClient!.SetAccessAsync(access);
    }

    public async Task<List<TraderAccessModel>> GetManagerAccess(string managerId)
    {
        var dataStream =
            _managerAccessClient!
                .GetManagerAccess(new() { ManagerId = managerId })
                .ResponseStream;

        var data = new List<TraderAccessModel>();
        while (await dataStream.MoveNext())
        {
            data.Add(dataStream.Current);
        }
        return data;
    }

    public async Task<TraderManagers> GetTraderManagersLookup(string traderId)
    {
        var dataStream =
            _managerAccessClient!
                .GetTraderManagers(new() { TraderId = traderId })
                .ResponseStream;

        var data = new TraderManagers();
        while (await dataStream.MoveNext())
        {
            data.Add(dataStream.Current.ManagerType, dataStream.Current.ManagerId);
        }
        return data;
    }

    public async Task<Dictionary<string, TraderManagers>> SearchTraderManagersAsync(SearchManager search)
    {


        var dataStream =
            _managerAccessClient!.Search(search).ResponseStream;

        var data = new Dictionary<string, TraderManagers>();
        while (await dataStream.MoveNext())
        {
            var traderId = dataStream.Current.TraderId!;
            data.TryAdd(dataStream.Current.TraderId, new ());
            data[traderId].Add(dataStream.Current.ManagerType, dataStream.Current.ManagerId);
        }
        return data;

    }

    #endregion

    #region Personal Data

    public async Task<List<PersonalDataModel>> SearchPersonalDataAsync(SearchPersonal search)
    {
        var stream = _personalDataClient!.Search(search).ResponseStream;
        var data = new List<PersonalDataModel>();
        while (await stream.MoveNext())
        {
            data.Add(stream.Current);
        }
        return data;

    }

    public async Task<PersonalDataModel> GetTraderPersonalDataAsync(string traderId)
    {
        var request = new GetPersonalDataRequest { Id = traderId };
        var clientData = await _personalDataClient!.GetAsync(request);
        return clientData.PersonalDataModel;
    }

    public async Task SetTraderPersonalData(PersonalDataModel model)
    {
        var request = new SetPersonalDataRequest
        {
            PersonalDataModel = model
        };
        await _personalDataClient!.SetAsync(request);
    }

    #endregion

    #region KYC

    public async Task SetClientKycStatusAsync(string clientId, KycStatus status, string agentId)
    {
        var request = new UpdateStatusRequest
        {
            ClientId = clientId,
            Status = status,
            Who = agentId
        };
        await _kycStatusClient!.UpdateAsync(request);
    }

    public async Task<KycStatus> GetClientKycStatusAsync(string clientId)
    {
        var request = new GetStatusRequest { ClientId = clientId};
        var clientData = await _kycStatusClient!.GetAsync(request);
        return clientData.Status;
    }

    public async Task<List<DocumentItemModel>> GetClientDocumentsList(string clientId)
    {
        var request = new GetDocumentsListRequest { ClientId = clientId };
        var stream = _documentsClient!.GetDocumentsList(request).ResponseStream;
        var data = new List<DocumentItemModel>();
        while (await stream.MoveNext())
        {
            data.Add(stream.Current);
        }
        return data;
    }

    public async Task<DocumentModel> GetClientDocument(string clientId, string documentId)
    {
        var request = new GetDocumentsRequest()
        {
            ClientId = clientId,
            DocIds = { documentId }
        };
        var stream = _documentsClient!.Get(request).ResponseStream;
        await stream.MoveNext();
        return stream.Current;
    }

    public void UpdateDocumentComment(string id, string comment, string authorId, string traderId)
    {
        var request = new UpdateCommentRequest
        {
            ClientId = traderId,
            DocId = id,
            Comment = comment,
            Who = authorId
        };
        _documentsClient!.UpdateComment(request);
    }

    public async Task UpdateDocumentStatus(string id, DocumentStatus status,
        DocumentRejectReason reason, string authorId, string traderId)
    {
        if (status != DocumentStatus.Rejected)
        {
            reason = DocumentRejectReason.NotSet;
        }
        var request = new UpdateDocumentStatusRequest
        {
            ClientId = traderId,
            DocId = id,
            Status = status,
            RejectReason = reason,
            Who = authorId
        };
        await _documentsClient!.UpdateStatusAsync(request);

    }

    public async Task<DocumentItemModel> UploadDocument(string authorId, string traderId, DocType docType,
        string content, string contentType, string comment)
    {
        var request = new UploadDocumentRequest
        {
            ClientId = traderId,
            DocType = docType,
            ContentType = contentType,
            Content = ByteString.FromBase64(content),
            Who = authorId,
            Expires = 0,
            Comment = comment
        };
        return await _documentsClient!.UploadAsync(request);
    }

    public async Task<DocumentItemModel> UploadDocument(UploadDocumentRequest request)
    {
        return await _documentsClient!.UploadAsync(request);
    }

    public async Task<List<ChangeLog>> GetKycChangeLogs(string traderId)
    {
        var stream = _kycChangeLogsClient!.Get(new GetChangeLogRequest { ClientId = traderId }).ResponseStream;
        var data = new List<ChangeLog>();
        while (await stream.MoveNext())
        {
            data.Add(stream.Current);
        }
        return data;
    }

    #endregion

    #region Balance

    public async Task<AccountManagerUpdateAccountBalanceGrpcResponse> UpdateAccountBalanceAsync(UpdateBalanceModel requestModel)
    {
        var res = await _accountsManagerClient!.UpdateClientAccountBalanceAsync(new()
        {
            AccountId = requestModel.AccountId,
            AllowNegativeBalance = requestModel.AllowNegativeBalance,
            Comment = requestModel.Comment,
            Delta = requestModel.Delta,
            ProcessId = GetProcessId(),
            Reason = requestModel.Reason,
            ReferenceTransactionId = requestModel.ReferenceTransactionId,
            TraderId = requestModel.TraderId
        });
        return res;
    }

    public async Task<List<ReportOperationHistoryItem>> GetOperationsHistoryAsync(string accountId, string traderId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId,
            TraderId = traderId,
        };
        if (from != DateTime.MinValue)
        {
            request.DateFrom = (ulong)from.UnixTime();
        }
        if (to != DateTime.MinValue)
        {
            request.DateTo = (ulong)to.UnixTime();
        }
        var responseStream = _reportClient!.GetOperationsHistoryInDateRange(request).ResponseStream;
        var operations = new List<ReportOperationHistoryItem>();
        while (await responseStream.MoveNext())
        {
            operations.Add(responseStream.Current);
        }
        return operations;
    }

    public async Task<Dictionary<string, Dictionary<string,List<ReportOperationHistoryItem>>>> SearchOperationsHistoryAsync(SearchHistory search)
    {
        var dataStream =
            _reportClient!.SearchHistoryPositions(search).ResponseStream;

        var data = new Dictionary<string, Dictionary<string,List<ReportOperationHistoryItem>>>();
        while (await dataStream.MoveNext())
        {
            var traderId = dataStream.Current.TraderId!;
            var accountId = dataStream.Current.AccountId!;
            data.TryAdd(traderId, new ()); 
            data[traderId].TryAdd(accountId,new());
            data[traderId][accountId].Add(dataStream.Current);
        }
        return data;

    }
    public async Task<List<AccountBalanceModel>> GetBalanceHistoryAsync(string accountId, string traderId)
    {
        var page = 1;
        var size = 100;
        var initResult = await GetBalanceHistoryPageAsync(accountId, traderId, page, size);
        var items = initResult.History;
        while (items.Count < (int)initResult.TotalItems)
        {
            page++;
            var res = await GetBalanceHistoryPageAsync(accountId, traderId, page, size);
            items.AddRange(res.History);
        }

        return items.Select(AccountBalanceModel.FromGrpc).ToList();
    }

    public async Task<ReportFlowsOperationsGetHistoryPaginatedGrpcResponse> GetBalanceHistoryPageAsync(
        string accountId, string traderId, int page, int size = 100)
    {
        var res = await _reportClient!.GetHistoryPaginatedAsync(new()
        {
            AccountId = accountId,
            TraderId = traderId,
            Page = page,
            Size = size
        });

        return res;
    }


    #endregion

    #region Active Positions

    public async Task<List<InvestmentPositionModel>> GetActivePositionsAsync(string accountId, string traderId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId,
            TraderId = traderId
        };
        if (from == DateTime.MinValue || from == DateTime.MinValue)
            return await GetActivePositionsAsync(request);
        request.DateFrom = (ulong)from.UnixTime();
        request.DateTo = (ulong)to.UnixTime();
        return await GetActivePositionsAsync(request);
    }

    public async Task<List<InvestmentPositionModel>> GetAllActivePositionsAsync(DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            DateFrom = (ulong)from.UnixTime(),
            DateTo = (ulong)to.UnixTime()
        };
        return await GetActivePositionsAsync(request);
    }

    private async Task<List<InvestmentPositionModel>> GetActivePositionsAsync(
        ReportFlowsOperationsGetInDateRangeGrpcRequest request)
    {
        var activePositionsStream = _reportClient!.GetActivePositionsInDateRange(request).ResponseStream;
        var positions = new List<InvestmentPositionModel>();
        while (await activePositionsStream.MoveNext())
        {
            var order = InvestmentPositionModel.FromGrpc(activePositionsStream.Current);
            _priceService.UpdateOrderProfit(order);
            positions.Add(order);
        }
        return positions;
    }

    #endregion

    #region Closed Positions

    public async Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(string accountId, string traderId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId,
            TraderId = traderId,
        };
        if (from != DateTime.MinValue && from != DateTime.MinValue)
        {
            request.DateFrom = (ulong)from.UnixTime();
            request.DateTo = (ulong)to.UnixTime();
        }
        return await GetHistoryPositionsAsync(request);
    }

    public async Task<List<InvestmentPositionModel>> GetAllHistoryPositionsAsync(DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            DateFrom = (ulong)from.UnixTime(),
            DateTo = (ulong)to.UnixTime()
        };
        return await GetHistoryPositionsAsync(request);
    }

    private async Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(
        ReportFlowsOperationsGetInDateRangeGrpcRequest request)
    {
        var activePositionsStream = _reportClient!.GetHistoryPositionsInDateRange(request).ResponseStream;
        var positions = new List<InvestmentPositionModel>();
        while (await activePositionsStream.MoveNext())
        {
            //var a = activePositionsStream.Current;
            positions.Add(InvestmentPositionModel.FromGrpc(activePositionsStream.Current));
        }
        return positions;
    }

    #endregion

    #region Trade Log

    public async Task<List<TradeLogItem>> GetTradeLog(string accountId, string traderId, DateTime from, DateTime to)
    {
        var tradeLogStream = _tradeLogClient!.Read(new()
        {
            AccountId = accountId,
            TraderId = traderId,
            DateFromMicros = from.ToEpochMic(),
            DateToMicros = to.ToEpochMic()
        }).ResponseStream;
        var logItems = new List<TradeLogItem>();
        while (await tradeLogStream.MoveNext())
        {
            logItems.Add(tradeLogStream.Current);
        }
        return logItems;
    }


    #endregion

    #region Withdrawals service

    public async Task<List<WithdrawalGrpcModel>> GetActiveWithdrawalRequestsAsync(string? traderId)
    {
        var request = traderId is null
            ? new GetWithdrawalsRequest()
            : new GetWithdrawalsRequest { TraderId = traderId };
        var stream = _withdrawalsClient!.GetActiveWithdrawals(request).ResponseStream;
        return await GetItemsList<WithdrawalGrpcModel>(stream);
    }

    public async Task<List<WithdrawalGrpcModel>> GetProcessedWithdrawalRequestsAsync(string? traderId)
    {
        var request = traderId is null
            ? new GetWithdrawalsRequest()
            : new GetWithdrawalsRequest { TraderId = traderId };
        var stream = _withdrawalsClient!.GetProcessedWithdrawals(request).ResponseStream;
        return await GetItemsList<WithdrawalGrpcModel>(stream);
    }

    public async Task<List<WithdrawalGrpcModel>> GetCanceledWithdrawalRequestsAsync(string? traderId)
    {
        var request = traderId is null
            ? new GetWithdrawalsRequest()
            : new GetWithdrawalsRequest { TraderId = traderId };
        var stream = _withdrawalsClient!.GetCanceledWithdrawals(request).ResponseStream;
        return await GetItemsList<WithdrawalGrpcModel>(stream);
    }

    public async Task ApproveWithdrawalRequestsAsync(string id, string traderId, string accountId, string agentId)
    {
        await _withdrawalsClient!.ProcessActiveWithdrawalAsync(new ProcessActiveWithdrawalRequest
        {
            ProcessId = GetProcessId(),
            TraderId = traderId,
            AccountId = accountId,
            Id = id,
            Who = agentId
        });
    }

    public async Task DenyWithdrawalRequestsAsync(string id, string traderId, string accountId, string agentId)
    {
        await _withdrawalsClient!.CancelActiveWithdrawalAsync(new CancelActiveWithdrawalRequest
        {
            ProcessId = GetProcessId(),
            TraderId = traderId,
            AccountId = accountId,
            Id = id,
            Who = agentId
        });
    }

    public async Task<List<WithdrawalGrpcModel>> SearchWithdrawals(IEnumerable<string> traderIds, WithdrawalStatus status)
    {
        var request = new SearchWithdrawalsRequest
        {
            Status = WithdrawalStatus.Pending
        };
        request.TraderIds.AddRange(traderIds);
        var stream = _withdrawalsClient!.SearchWithdrawals(request).ResponseStream;
        return await GetItemsList<WithdrawalGrpcModel>(stream);
    }

    #endregion

    #region Helper Functions

    private async Task<List<T>> GetItemsList<T>(IAsyncStreamReader<T> stream) where T : class
    {
        var items = new List<T>();
        while (await stream.MoveNext())
        {
            items.Add(stream.Current);
        }
        return items;
    }

    private string GetProcessId()
    {
        return $"Backoffice-{FormatUtils.DateTimeNamedWithMsFormat(DateTime.UtcNow)}";
    }

    public async Task<string> GetClientRedirectUrl(string clientId)
    {
        var redirectUrl = await _authServiceClient.GeneratePlatformRedirectUrlAsync(new GeneratePlatformRedirectUrlGrpcRequest
        {
            TraderId = clientId
        });

        return redirectUrl.RedirectUrl;
    }

    #endregion
}