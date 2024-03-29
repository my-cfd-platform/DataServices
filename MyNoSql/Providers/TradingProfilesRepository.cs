﻿using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class TradingProfilesRepository : IRepository<ITradingProfile>
{
    private readonly IMyNoSqlServerDataWriter<TradingProfileMyNoSqlEntity> _table;
    private const string TableName = "tradingprofiles";
    private const string LivePrefix = "live-";
    private const string DemoPrefix = "demo-";
    public TradingProfilesRepository(string url, bool isLive)
    {
        var table = new MyNoSqlServerDataWriter<TradingProfileMyNoSqlEntity>(
            () => url,
            isLive? $"{LivePrefix}{TableName}" : $"{DemoPrefix}{TableName}",
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<ITradingProfile>> GetAllAsync()
    {
        var partitionKey = TradingProfileMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<ITradingProfile>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<ITradingProfile> GetAsync(string key)
    {
        var partitionKey = TradingProfileMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<ITradingProfile> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(ITradingProfile item)
    {
        var entity = TradingProfileMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(TradingProfileMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(ITradingProfile item)
    {
        await DeleteAsync(item.Id);
    }

    public Task<int> GetCountAsync()
    {
        throw new NotImplementedException();
    }
}