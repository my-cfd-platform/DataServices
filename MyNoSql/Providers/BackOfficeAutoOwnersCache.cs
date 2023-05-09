using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeAutoOwnersCache : ICache<IBackofficeAutoOwner>
{
    private readonly IMyNoSqlServerDataReader<BackofficeAutoOwnerMyNoSqlEntity> _readRepository;
    private const string TableName = "backoffice-autoowner";

    public BackOfficeAutoOwnersCache(IMyNoSqlServerDataReader<BackofficeAutoOwnerMyNoSqlEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public BackOfficeAutoOwnersCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<BackofficeAutoOwnerMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IBackofficeAutoOwner> GetAll()
    {
        var partitionKey = BackofficeAutoOwnerMyNoSqlEntity.GeneratePartitionKey();
        return _readRepository.Get(partitionKey);
    }

    public IBackofficeAutoOwner Get(string id)
    {
        var partitionKey = BackofficeAutoOwnerMyNoSqlEntity.GeneratePartitionKey();
        var rowKey = BackofficeAutoOwnerMyNoSqlEntity.GenerateRowKey(id);
        return _readRepository.Get(partitionKey, rowKey);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IBackofficeAutoOwner>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}