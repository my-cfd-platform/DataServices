using DataServices.Models;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

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
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IBackofficeUser> GetAsync(string key)
    {
        var partitionKey = BackofficeUserMyNoSqlEntity.GeneratePartitionKey();
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IBackofficeUser item)
    {
        var entity = BackofficeUserMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }
}