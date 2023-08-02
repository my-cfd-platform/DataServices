using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;
using System.Collections.Concurrent;

namespace DataServices.MyNoSql.Providers;

public class ClientSearchCache : ICache<IClientSearch>
{
    private readonly IMyNoSqlServerDataReader<ClientSearchMyNoSqlEntity> _readRepository;
    private const string TableName = "client-search";

    public ClientSearchCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<ClientSearchMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IClientSearch> GetAll()
    {
        return _readRepository.Get(ClientSearchMyNoSqlEntity.GeneratePartitionKey());
    }

    public IEnumerable<IClientSearch> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IClientSearch Get(string id)
    {
        throw new NotImplementedException();
    }

    public IClientSearch Get(string id, string partitionKey)
    {
        var res = _readRepository.Get(partitionKey, condition: (s)=>s.Id == id);
        var status = res.FirstOrDefault();
        return status ?? default!;
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IClientSearch>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}