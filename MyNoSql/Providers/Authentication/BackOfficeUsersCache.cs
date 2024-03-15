using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Interfaces.Authentication;
using DataServices.MyNoSql.Models.Authentication;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers.Authentication;

public class BackOfficeUsersCache : ICache<IBackofficeUser>
{
    private readonly IMyNoSqlServerDataReader<BackofficeUserMyNoSqlEntity> _readRepository;
    private const string TableName = "backoffice-users";

    public BackOfficeUsersCache(IMyNoSqlServerDataReader<BackofficeUserMyNoSqlEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public BackOfficeUsersCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<BackofficeUserMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IBackofficeUser> GetAll()
    {
        var partitionKey = BackofficeUserMyNoSqlEntity.GeneratePartitionKey();
        return GetAll(partitionKey);
    }

    public IEnumerable<IBackofficeUser> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IBackofficeUser Get(string id)
    {
        var partitionKey = BackofficeUserMyNoSqlEntity.GeneratePartitionKey();
        var rowKey = BackofficeUserMyNoSqlEntity.GenerateRowKey(id);
        return Get(rowKey, partitionKey);
    }

    public IBackofficeUser Get(string id, string partitionKey)
    {
        return _readRepository.Get(partitionKey, id);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IBackofficeUser>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}