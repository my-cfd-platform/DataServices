using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class TradingInstrumentRepository : IRepository<ITradingInstrument>
{
    private readonly IMyNoSqlServerDataWriter<TradingInstrumentMyNoSqlEntity> _table;
    private const string TableName = "instruments";
    public TradingInstrumentRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<TradingInstrumentMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<ITradingInstrument>> GetAllAsync()
    {
        var partitionKey = TradingInstrumentMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<ITradingInstrument>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<ITradingInstrument> GetAsync(string key)
    {
        var partitionKey = TradingInstrumentMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<ITradingInstrument> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(ITradingInstrument item)
    {
        var entity = TradingInstrumentMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(TradingInstrumentMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(ITradingInstrument item)
    {
        await DeleteAsync(item.Id);
    }
}