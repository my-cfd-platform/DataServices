namespace DataServices.MyNoSql.Interfaces;

public interface IDefaultValuesService
{
    Task<string> GetValueAsync(string key, string suffix = "");
    Task<IEnumerable<string>> GetValuesAsync(string key, string suffix = "");
    Task SetValueAsync(string key, string value, string suffix = "");
    Task SetValuesAsync(string key, IEnumerable<string> values, string suffix = "");
}