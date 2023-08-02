using DataServices.Extensions;
using DataServices.Models;
using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;
using DataServices.Services.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;
using Pd;
using StatusFlows;

namespace DataServices.Services;

public class StatusesService : IStatusesService
{
    private readonly StatusService.StatusServiceClient? _statusClient;
    private readonly IRepository<IStatus> _statusRepository;
    private readonly ICache<IStatus> _statusCache;


    public StatusesService(DataServicesSettings settings, IRepository<IStatus> repository, ICache<IStatus> cache)
    {
        if (settings.StatusGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _statusClient = new StatusService.StatusServiceClient(
                GrpcChannel.ForAddress(settings.StatusGrpcServiceUrl));
        }

        _statusRepository = repository;
        _statusCache = cache;
    }

    public IEnumerable<IStatus> GetStatuses(string statusType)
    {
        return _statusCache.GetAll(statusType);
    }

    public string GetStatusName(string id, string statusType)
    {
        var status = _statusCache.Get(id, statusType);
        return status == null! ? null! : status.Name;
    }

    public async Task<Dictionary<string, Dictionary<string,StatusGrpcModel>>> SearchTraderStatusesAsync(SearchStatus search)
    {
        var dataStream =
            _statusClient!.Search(search).ResponseStream;

        var data = new Dictionary<string, Dictionary<string,StatusGrpcModel>>();
        while (await dataStream.MoveNext())
        {
            var traderId = dataStream.Current.Id!;
            data.TryAdd(traderId, new ());
            data[traderId].TryAdd(dataStream.Current.Name, dataStream.Current);
        }
        return data;

    }

    #region Trader Statuses
    
    //Statuses of a selected trader
    public async Task<TraderStatusLookup> GetTraderStatusesGrpc(string id)
    {
        var request = new RequestById { Id = id };
        var response = await _statusClient!.GetStatusesAsync(request);

        return new TraderStatusLookup
        {
            Statuses = response.Statuses.ToDictionary(k => k.Key, v => v.Value),
            Labels = response.Labels.ToDictionary(k => k.Key, v => v.Value),
        };
    }

    public async Task SetTraderStatus(string id, string status, string label, string statusType)
    {
        var request = new StatusGrpcModel
        {
            Id = id,
            Name = statusType,
            Value = status,
            Label = label
        };
        await _statusClient!.SetStatusAsync(request);
    }
    
    #endregion
}