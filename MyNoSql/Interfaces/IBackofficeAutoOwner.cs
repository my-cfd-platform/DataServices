using DataServices.MyNoSql.Enums;

namespace DataServices.MyNoSql.Interfaces;

public interface IBackofficeAutoOwner
{
    string Id { get; set; }
    public string OfficeId { get; set; }
    public string BrandId { get; set; }
    public IEnumerable<string> SupportedCountries { get; set; }
    public IEnumerable<TrafficSourceType> TrafficSourceTypes { get; set; }
    public IEnumerable<string> TrafficSourceKeys { get; set; }
}