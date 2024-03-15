using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class SwapProfileRepository : IRepository<ISwapProfile>
{
    private readonly IMyNoSqlServerDataWriter<SwapProfileMyNoSqlEntity> _table;
    private const string TableName = "swap-profile";
    public SwapProfileRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<SwapProfileMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<ISwapProfile>> GetAllAsync()
    {
        return await _table.GetAsync();
    }

    public async Task<IEnumerable<ISwapProfile>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }
/*
    public async ValueTask<IEnumerable<ISwapProfile>> GetAsync(string id)
    {

    }
*/
    public async Task UpdateAsync(ISwapProfile swapProfile)
    {
        var entity = SwapProfileMyNoSqlEntity.Create(swapProfile);
        await _table.InsertOrReplaceAsync(entity);
    }

    #region Not Implemented

    public Task<ISwapProfile> GetAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task<ISwapProfile> GetAsync(string key, string partitionKey)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(ISwapProfile item)
    {
        var rowKey = SwapProfileMyNoSqlEntity.GenerateRowKey(item.InstrumentId);
        var partitionKey = SwapProfileMyNoSqlEntity.GeneratePartitionKey(item.Id);
        await _table.DeleteAsync(partitionKey, rowKey);
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }

    #endregion

}