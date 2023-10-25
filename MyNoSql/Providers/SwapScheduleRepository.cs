using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class SwapScheduleRepository : IRepository<ISwapSchedule>
{
    private readonly IMyNoSqlServerDataWriter<SwapScheduleMyNoSqlEntity> _table;
    private const string TableName = "swap-schedule";
    public SwapScheduleRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<SwapScheduleMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<ISwapSchedule>> GetAllAsync()
    {
        return await _table.GetAsync();
    }

    public async Task<IEnumerable<ISwapSchedule>> GetAllAsync(string swapProfileId)
    {
        return await _table.GetAsync(swapProfileId);
    }

    public async Task UpdateAsync(ISwapSchedule item)
    {
        var entity = SwapScheduleMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    /*
    //todo implement this delete logic if delete will be required
    public async Task DeleteAsync(string scheduleId, DayOfWeek dayOfWeek, TimeSpan time)
    {
        var partitionKey = SwapScheduleMyNoSqlEntity.GeneratePartitionKey(scheduleId);
        var rowKey = SwapScheduleMyNoSqlEntity.GenerateRowKey(dayOfWeek, time);
        await _table.DeleteAsync(partitionKey, rowKey);
    }
    */
    public Task<ISwapSchedule> GetAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task<ISwapSchedule> GetAsync(string key, string partitionKey)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(ISwapSchedule item)
    {
        var rowKey = SwapScheduleMyNoSqlEntity.GenerateRowKey(item);
        var partitionKey = SwapScheduleMyNoSqlEntity.GeneratePartitionKey(item);
        await _table.DeleteAsync(partitionKey, rowKey);
    }
}