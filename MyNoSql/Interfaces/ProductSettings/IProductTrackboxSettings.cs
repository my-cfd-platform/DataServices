namespace DataServices.MyNoSql.Interfaces.ProductSettings;

public interface IProductTrackboxSettings
{
    public string Id { get; }
    public List<string> ApiKeys { get; }
}