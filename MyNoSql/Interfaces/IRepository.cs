using System.Collections.Concurrent;

namespace DataServices.MyNoSql.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(string partitionKey);
    Task<T> GetAsync(string key);
    Task<T> GetAsync(string key, string partitionKey);
    Task UpdateAsync(T item);
    Task DeleteAsync(string key);
    Task DeleteAsync(T item);
}