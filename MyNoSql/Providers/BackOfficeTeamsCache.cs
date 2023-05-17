using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeTeamsCache : ICache<IBackofficeTeam>
{
    private readonly IMyNoSqlServerDataReader<BackofficeTeamMyNoSqlEntity> _readRepository;
    private const string TableName = "backoffice-teams";

    public BackOfficeTeamsCache(IMyNoSqlServerDataReader<BackofficeTeamMyNoSqlEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public BackOfficeTeamsCache(IMyNoSqlSubscriber tcpConnection)
    {
        var readRepository =
            new MyNoSqlReadRepository<BackofficeTeamMyNoSqlEntity>(tcpConnection, TableName);
        _readRepository = readRepository;
    }

    public IEnumerable<IBackofficeTeam> GetAll()
    {
        var partitionKey = BackofficeTeamMyNoSqlEntity.GeneratePartitionKey();
        return GetAll(partitionKey);
    }

    public IEnumerable<IBackofficeTeam> GetAll(string partitionKey)
    {
        return _readRepository.Get(partitionKey);
    }

    public IBackofficeTeam Get(string id)
    {
        var partitionKey = BackofficeTeamMyNoSqlEntity.GeneratePartitionKey();
        var rowKey = BackofficeTeamMyNoSqlEntity.GenerateRowKey(id);
        return Get(rowKey, partitionKey);
    }

    public IBackofficeTeam Get(string id, string partitionKey)
    {
        return _readRepository.Get(partitionKey, id);
    }

    public void SubscribeOnChanges(Type type, Action<IReadOnlyList<IBackofficeTeam>> changes)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeOnChanges(Type type)
    {
        throw new NotImplementedException();
    }
}