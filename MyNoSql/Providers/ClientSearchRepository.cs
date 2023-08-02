using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class ClientSearchRepository : IRepository<IClientSearch>
{
    private readonly IMyNoSqlServerDataWriter<ClientSearchMyNoSqlEntity> _table;
    private const string TableName = "client-search";

    public ClientSearchRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<ClientSearchMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }



    public async Task<IEnumerable<IClientSearch>> GetAllAsync()
    {
        return await _table.GetAsync();
    }

    public async Task<IEnumerable<IClientSearch>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public Task<IClientSearch> GetAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<IClientSearch> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IClientSearch item)
    {
        var entity = ClientSearchMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(ClientSearchMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IClientSearch item)
    {
        await DeleteAsync(item.Id);
    }
}