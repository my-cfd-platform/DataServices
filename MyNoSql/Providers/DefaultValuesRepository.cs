using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class DefaultValuesRepository : IRepository<IDefaultValues>
{
    private readonly IMyNoSqlServerDataWriter<DefaultValuesEntity> _table;
    private const string TableName = "defaultvalues";
    public DefaultValuesRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<DefaultValuesEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IDefaultValues>> GetAllAsync()
    {
        var partitionKey = DefaultValuesEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey);
    }

    public async Task UpdateAsync(IDefaultValues item)
    {
        var entity = DefaultValuesEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task<IDefaultValues> GetAsync(string key)
    {
        var partitionKey = DefaultValuesEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(DefaultValuesEntity.GeneratePartitionKey(), key);
    }
}