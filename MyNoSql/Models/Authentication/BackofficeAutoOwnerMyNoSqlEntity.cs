using DataServices.Extensions;
using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces.Authentication;
using MyNoSqlServer.Abstractions;

namespace DataServices.MyNoSql.Models.Authentication;

public class BackofficeAutoOwnerMyNoSqlEntity : MyNoSqlDbEntity, IBackofficeAutoOwner
{
    public static string GeneratePartitionKey() => "ba";

    public static string GenerateRowKey(string id) => id;

    public string Id
    {
        get => RowKey;
        set => RowKey = value;
    }

    public string OfficeId { get; set; }
    public string BrandId { get; set; }
    public IEnumerable<string> SupportedCountries { get; set; }
    public IEnumerable<TrafficSourceType> TrafficSourceTypes { get; set; }
    public IEnumerable<string> TrafficSourceKeys { get; set; }


    public static BackofficeAutoOwnerMyNoSqlEntity Create(IBackofficeAutoOwner src)
    {
        if (src.Id.IsNullOrEmpty())
            src.Id = Guid.NewGuid().ToString();
        return new BackofficeAutoOwnerMyNoSqlEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Id),
            OfficeId = src.OfficeId,
            BrandId = src.BrandId,
            SupportedCountries = src.SupportedCountries,
            TrafficSourceTypes = src.TrafficSourceTypes,
            TrafficSourceKeys = src.TrafficSourceKeys,
        };
    }
}