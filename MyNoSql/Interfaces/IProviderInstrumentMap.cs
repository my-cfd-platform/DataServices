namespace DataServices.MyNoSql.Interfaces;

public interface IProviderInstrumentMap
{
    public string Id { get; }
    public string LpId { get; set; }
    public IDictionary<string, string> Map { get; set; }
}