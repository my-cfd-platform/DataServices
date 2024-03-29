﻿using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Interfaces.Authentication;
using DataServices.MyNoSql.Models.Authentication;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers.Authentication;

public class BackOfficeUsersRepository : IRepository<IBackofficeUser>
{
    private readonly IMyNoSqlServerDataWriter<BackofficeUserMyNoSqlEntity> _table;
    private const string TableName = "backoffice-users";
    public BackOfficeUsersRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<BackofficeUserMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IBackofficeUser>> GetAllAsync()
    {
        var partitionKey = BackofficeUserMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IBackofficeUser>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeUser> GetAsync(string key)
    {
        var partitionKey = BackofficeUserMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(partitionKey, key);
    }

    public async Task<IBackofficeUser> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeUser item)
    {
        var entity = BackofficeUserMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(BackofficeUserMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IBackofficeUser item)
    {
        await DeleteAsync(item.Id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _table.GetCountAsync(BackofficeUserMyNoSqlEntity.GeneratePartitionKey());
    }
}