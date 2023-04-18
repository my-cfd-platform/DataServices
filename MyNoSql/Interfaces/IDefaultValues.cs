namespace DataServices.MyNoSql.Interfaces;

public interface IDefaultValues
{
    public string Id { get; }
    public string Value { get; }
    public IEnumerable<string> Values { get; }
}