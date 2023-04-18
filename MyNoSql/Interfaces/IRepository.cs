namespace DataServices.MyNoSql.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(string key);
    Task UpdateAsync(T item);
}