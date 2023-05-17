using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class InstrumentMappingRepository : IRepository<IProviderInstrumentMap>
{
    private readonly IMyNoSqlServerDataWriter<ProviderInstrumentEntity> _table;
    private const string TableName = "instrument-mapping";
    public InstrumentMappingRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<ProviderInstrumentEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IProviderInstrumentMap>> GetAllAsync()
    {
        var partitionKey = ProviderInstrumentEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IProviderInstrumentMap>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IProviderInstrumentMap> GetAsync(string key)
    {
        var partitionKey = ProviderInstrumentEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<IProviderInstrumentMap> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IProviderInstrumentMap item)
    {
        var entity = ProviderInstrumentEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(ProviderInstrumentEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IProviderInstrumentMap item)
    {
        await DeleteAsync(item.Id);
    }
}