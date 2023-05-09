using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeRolesCache : ICache<IBackofficeRole>
{
    private readonly IMyNoSqlServerDataReader<BackofficeRoleMyNoSqlEntity> _readRepository;
    private const string TableName = "backoffice-roles";

    public BackOfficeRolesCache(IMyNoSqlServerDataReader<BackofficeRoleMyNoSqlEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public BackOfficeRolesCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<BackofficeRoleMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IBackofficeRole> GetAll()
    {
        var partitionKey = BackofficeRoleMyNoSqlEntity.GeneratePartitionKey();
        return _readRepository.Get(partitionKey);
    }

    public IBackofficeRole Get(string id)
    {
        var partitionKey = BackofficeRoleMyNoSqlEntity.GeneratePartitionKey();
        var rowKey = BackofficeRoleMyNoSqlEntity.GenerateRowKey(id);
        return _readRepository.Get(partitionKey, rowKey);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IBackofficeRole>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}