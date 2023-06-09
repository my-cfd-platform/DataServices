using AccountsManager;
using AuthenticationIntegration;
using DataServices.Extensions;
using DataServices.Models;
using DataServices.Models.Auth.Users;
using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;
using DataServices.Services.Interfaces;
using DataServices.Utils;
using DotNetCoreDecorators;
using Grpc.Core;
using Grpc.Net.Client;
using Keyvalue;
using ManagerAccessFlows;
using Pd;
using ReportGrpc;
using TradeLog;
using TraderCredentials;

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

    private IPriceService _priceService;

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
    }

    #region Trader and Account

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

        return (from model in allTraderBrands
                let canAccess = _managerAccessClient!.CanAccess(new()
                { ManagerId = user.Id, TraderId = model.TraderId })
                where canAccess.Value
                select model).ToList();
    }

    public async Task<List<TradingAccountModel>> GetTraderAccountsAsync(string traderId)
    {
        var traderAccountsStream =
            _accountsManagerClient!
                .GetClientAccounts(new() { TraderId = traderId })
                .ResponseStream;

        var accounts = new List<TradingAccountModel>();
        while (await traderAccountsStream.MoveNext())
        {
            accounts.Add(TradingAccountModel.FromGrpc(traderAccountsStream.Current));
        }
        return accounts;
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

    #endregion

    #region KeyValue Service

    public async Task<List<GetKeyValueGrpcResponseModel>> GetClientKeyValues(string clientId)
    {
        var keyValuesList = new List<GetKeyValueGrpcResponseModel>();
        var keyValuesStream = _keyValueClient
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

    public async Task<bool> CanManagerAccess(string managerId, string traderId)
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

    #endregion

    #region Personal Data

    public async Task<PersonalDataModel> GetTraderPersonalDataAsync(TraderBrandModel traderBrand)
    {
        var request = new GetPersonalDataRequest { Id = traderBrand.TraderId };
        var clientData = await _personalDataClient!.GetAsync(request);
        return clientData.PersonalDataModel;
    }

    public async Task SetTraderPersonalData(PersonalDataModel model, string traderId)
    {
        var request = new SetPersonalDataRequest
        {
            Id = traderId,
            PersonalDataModel = model
        };
        await _personalDataClient!.SetAsync(request);
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

    public async Task<List<ReportOperationHistoryItem>> GetOperationsHistoryAsync(string accountId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId
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

    public async Task<List<AccountBalanceModel>> GetBalanceHistoryAsync(string accountId)
    {
        var page = 1;
        var size = 100;
        var initResult = await GetBalanceHistoryPageAsync(accountId, page, size);
        var items = initResult.History;
        while (items.Count < (int)initResult.TotalItems)
        {
            page++;
            var res = await GetBalanceHistoryPageAsync(accountId, page, size);
            items.AddRange(res.History);
        }

        return items.Select(AccountBalanceModel.FromGrpc).ToList();
    }

    public async Task<ReportFlowsOperationsGetHistoryPaginatedGrpcResponse> GetBalanceHistoryPageAsync(
        string accountId, int page, int size = 100)
    {
        var res = await _reportClient!.GetHistoryPaginatedAsync(new()
        {
            AccountId = accountId,
            Page = page,
            Size = size
        });

        return res;
    }


    #endregion

    #region Active Positions

    public async Task<List<InvestmentPositionModel>> GetActivePositionsAsync(string accountId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId
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

    public async Task<List<InvestmentPositionModel>> GetHistoryPositionsAsync(string accountId, DateTime from, DateTime to)
    {
        var request = new ReportFlowsOperationsGetInDateRangeGrpcRequest
        {
            AccountId = accountId
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
            var a = activePositionsStream.Current;
            positions.Add(InvestmentPositionModel.FromGrpc(activePositionsStream.Current));
        }
        return positions;
    }

    #endregion

    private string GetProcessId()
    {
        return $"Backoffice-{FormatUtils.DateTimeNamedWithMsFormat(DateTime.UtcNow)}";
    }

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

    public async Task<string> GetClientRedirectUrl(string clientId)
    {
        var redirectUrl = await _authServiceClient.GeneratePlatformRedirectUrlAsync(new GeneratePlatformRedirectUrlGrpcRequest
        {
            TraderId = clientId
        });

        return redirectUrl.RedirectUrl;
    }

}