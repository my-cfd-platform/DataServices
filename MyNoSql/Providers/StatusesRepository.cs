using DataServices.DefaultData;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class StatusesRepository : IRepository<IStatus>
{
    private IMyNoSqlServerDataWriter<StatusMyNoSqlEntity> _table = null!;
    private IMyNoSqlServerDataWriter<MetaMyNoSqlEntity> _metaTable = null!;
    private const string TableName = "statuses";
    private const string MetaTableName = $"{TableName}-meta";

    public StatusesRepository(string url)
    {
        Init(url).GetAwaiter().GetResult();;
    }

    private async Task Init(string url)
    {
        await InitTables(url);
        await InitData();
    }

    private async Task InitTables(string url)
    {
        var table = new MyNoSqlServerDataWriter<StatusMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        await table.CreateTableIfNotExistsAsync().ConfigureAwait(false);
        _table = table;

        var metaTable = new MyNoSqlServerDataWriter<MetaMyNoSqlEntity>(
            () => url,
            MetaTableName,
            true);
        // create table if not exists
        await metaTable.CreateTableIfNotExistsAsync();
        _metaTable = metaTable;
    }

    private async Task InitData()
    {
        var metaData = (await _metaTable.GetAsync()).Select(m=>m.Id).ToList();

        foreach (var (partitionKey, statuses) in StatusesDefaultData.Data)
        {
            if (metaData.Contains(partitionKey))
            {
                continue;
            }

            await _table.BulkInsertOrReplaceAsync(statuses.Select(StatusMyNoSqlEntity.Create).ToList());
            await _metaTable.InsertOrReplaceAsync(MetaMyNoSqlEntity.Create(new MetaMyNoSqlEntity
                { Id = partitionKey }));
        }
    }

    public async Task<IEnumerable<IStatus>> GetAllAsync()
    {
        return await _table.GetAsync();
    }

    public async Task<IEnumerable<IStatus>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public Task<IStatus> GetAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<IStatus> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IStatus item)
    {
        var entity = StatusMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public Task DeleteAsync(string key)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(IStatus item)
    {
        await _table.DeleteAsync(item.Type, item.Id);
    }
}