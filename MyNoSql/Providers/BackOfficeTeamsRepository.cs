using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeTeamsRepository : IRepository<IBackofficeTeam>
{
    private readonly IMyNoSqlServerDataWriter<BackofficeTeamMyNoSqlEntity> _table;
    private const string TableName = "backoffice-teams";
    public BackOfficeTeamsRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<BackofficeTeamMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IBackofficeTeam>> GetAllAsync()
    {
        var partitionKey = BackofficeTeamMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeTeam> GetAsync(string key)
    {
        var partitionKey = BackofficeTeamMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeTeam item)
    {
        var entity = BackofficeTeamMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(BackofficeTeamMyNoSqlEntity.GeneratePartitionKey(), key);
    }
}