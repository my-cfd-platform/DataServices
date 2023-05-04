using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeAutoOwnerRepository : IRepository<IBackofficeAutoOwner>
{
    private readonly IMyNoSqlServerDataWriter<BackofficeAutoOwnerMyNoSqlEntity> _table;
    private const string TableName = "backoffice-office";
    public BackOfficeAutoOwnerRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<BackofficeAutoOwnerMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IBackofficeAutoOwner>> GetAllAsync()
    {
        var partitionKey = BackofficeAutoOwnerMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeAutoOwner> GetAsync(string key)
    {
        var partitionKey = BackofficeAutoOwnerMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeAutoOwner item)
    {
        var entity = BackofficeAutoOwnerMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(BackofficeAutoOwnerMyNoSqlEntity.GeneratePartitionKey(), key);
    }
}