using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeRolesRepository : IRepository<IBackofficeRole>
{
    private readonly IMyNoSqlServerDataWriter<BackofficeRoleMyNoSqlEntity> _table;
    private const string TableName = "backoffice-roles";
    public BackOfficeRolesRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<BackofficeRoleMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IBackofficeRole>> GetAllAsync()
    {
        var partitionKey = BackofficeRoleMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IBackofficeRole>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeRole> GetAsync(string key)
    {
        var partitionKey = BackofficeRoleMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<IBackofficeRole> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeRole item)
    {
        var entity = BackofficeRoleMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(BackofficeRoleMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IBackofficeRole item)
    {
        await DeleteAsync(item.Id);
    }
}