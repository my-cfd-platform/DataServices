using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DataServices.Models.Backoffice;

public class AutoOwnerProfileModel : IBackofficeAutoOwner
{
    public string Id { get; set; }
    [Required]
    public string OfficeId { get; set; }
    [Required]
    public string BrandId { get; set; }
    public IEnumerable<string> SupportedCountries { get; set; } = new List<string>();
    public IEnumerable<TrafficSourceType> TrafficSourceTypes { get; set; } = new List<TrafficSourceType>();
    public IEnumerable<string> TrafficSourceKeys { get; set; } = new List<string>();

    public static AutoOwnerProfileModel ToModel(IBackofficeAutoOwner src)
    {
        return new AutoOwnerProfileModel
        {
            Id = src.Id,
            OfficeId = src.OfficeId,
            BrandId = src.BrandId,
            SupportedCountries = src.SupportedCountries,
            TrafficSourceTypes = src.TrafficSourceTypes,
            TrafficSourceKeys = src.TrafficSourceKeys,
        };
    }
}
