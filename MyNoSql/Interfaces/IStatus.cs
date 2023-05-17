namespace DataServices.MyNoSql.Interfaces;

public interface IStatus
{
    string Id { get; }
    string Name { get; set; }
    string Type { get; set; }
    public IEnumerable<string> Labels { get; set; }
}