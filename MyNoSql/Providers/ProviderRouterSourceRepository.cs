using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class ProviderRouterSourceRepository : IRepository<IProviderRouterSource>
{
    private readonly IMyNoSqlServerDataWriter<ProviderRouterSourceEntity> _table;
    private const string TableName = "provider-router-source";
    public ProviderRouterSourceRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<ProviderRouterSourceEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IProviderRouterSource>> GetAllAsync()
    {
        var partitionKey = ProviderRouterSourceEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IProviderRouterSource>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IProviderRouterSource> GetAsync(string key)
    {
        var partitionKey = ProviderRouterSourceEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<IProviderRouterSource> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IProviderRouterSource item)
    {
        var entity = ProviderRouterSourceEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(ProviderRouterSourceEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IProviderRouterSource item)
    {
        await DeleteAsync(item.Id);
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }
}