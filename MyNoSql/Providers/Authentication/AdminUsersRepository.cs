using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Interfaces.Authentication;
using DataServices.MyNoSql.Models.Authentication;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers.Authentication;

public class AdminUsersRepository : IRepository<IAdminUser>
{
    private readonly IMyNoSqlServerDataWriter<AdminUserMyNoSqlEntity> _table;
    private const string TableName = "admin-users";
    public AdminUsersRepository(string url)
    {
        var table = new MyNoSqlServerDataWriter<AdminUserMyNoSqlEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        table.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _table = table;
    }

    public async Task<IEnumerable<IAdminUser>> GetAllAsync()
    {
        var partitionKey = AdminUserMyNoSqlEntity.GeneratePartitionKey();
        return await GetAllAsync(partitionKey);
    }

    public async Task<IEnumerable<IAdminUser>> GetAllAsync(string partitionKey)
    {
        return await _table.GetAsync(partitionKey);
    }

    public async Task<IAdminUser> GetAsync(string key)
    {
        var partitionKey = AdminUserMyNoSqlEntity.GeneratePartitionKey();
        return await GetAsync(key, partitionKey);
    }

    public async Task<IAdminUser> GetAsync(string key, string partitionKey)
    {
        return await _table.GetAsync(partitionKey, key);
    }

    public async Task UpdateAsync(IAdminUser item)
    {
        var entity = AdminUserMyNoSqlEntity.Create(item);
        await _table.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(string key)
    {
        await _table.DeleteAsync(AdminUserMyNoSqlEntity.GeneratePartitionKey(), key);
    }

    public async Task DeleteAsync(IAdminUser item)
    {
        await DeleteAsync(item.Id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _table.GetCountAsync(AdminUserMyNoSqlEntity.GeneratePartitionKey());
    }
}