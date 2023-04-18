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
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IProviderInstrumentMap> GetAsync(string key)
    {
        var partitionKey = ProviderInstrumentEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IProviderInstrumentMap item)
    {
        var entity = ProviderInstrumentEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }
}