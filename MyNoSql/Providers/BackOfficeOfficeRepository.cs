﻿using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class BackOfficeOfficeRepository : IRepository<IBackofficeOffice>
{
    private readonly IMyNoSqlServerDataWriter<BackofficeOfficeMyNoSqlEntity> _table;
    private const string TableName = "backoffice-office";
    public BackOfficeOfficeRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<BackofficeOfficeMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IBackofficeOffice>> GetAllAsync()
    {
        var partitionKey = BackofficeOfficeMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeOffice> GetAsync(string key)
    {
        var partitionKey = BackofficeOfficeMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeOffice item)
    {
        var entity = BackofficeOfficeMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(BackofficeOfficeMyNoSqlEntity.GeneratePartitionKey(), key);
    }
}