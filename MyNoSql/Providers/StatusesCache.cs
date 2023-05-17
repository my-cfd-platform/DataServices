using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class StatusesCache : ICache<IStatus>
{
    private readonly IMyNoSqlServerDataReader<StatusMyNoSqlEntity> _readRepository;
    private const string TableName = "statuses";

    public StatusesCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<StatusMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IStatus> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IStatus> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IStatus Get(string id)
    {
        throw new NotImplementedException();
    }

    public IStatus Get(string id, string partitionKey)
    {
        var res = _readRepository.Get(partitionKey, condition: (s)=>s.Id == id);
        var status = res.FirstOrDefault();
        return status ?? default!;
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IStatus>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}