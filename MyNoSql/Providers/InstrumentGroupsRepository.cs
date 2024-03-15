using DataServices.Models;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class InstrumentGroupsRepository : IRepository<IInstrumentGroup>
{
    private readonly IMyNoSqlServerDataWriter<InstrumentGroupMyNoSqlEntity> _table;
    private const string TableName = "instrumentsgroups";
    public InstrumentGroupsRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<InstrumentGroupMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IInstrumentGroup>> GetAllAsync()
    {
        var partitionKey = InstrumentGroupMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IInstrumentGroup>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IInstrumentGroup> GetAsync(string key)
    {
        var partitionKey = InstrumentGroupMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<IInstrumentGroup> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IInstrumentGroup item)
    {
        var entity = InstrumentGroupMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(InstrumentGroupMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IInstrumentGroup item)
    {
        await DeleteAsync(item.Id);
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }
}