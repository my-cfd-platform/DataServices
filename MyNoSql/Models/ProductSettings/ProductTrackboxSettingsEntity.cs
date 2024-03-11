using DataServices.MyNoSql.Interfaces.ProductSettings;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace DataServices.MyNoSql.Models.ProductSettings;

public class ProductTrackboxSettingsEntity : MyNoSqlDbEntity, IProductTrackboxSettings
{ 
	public string Id => RowKey = "trackbox";

	[JsonProperty("api_keys")]
    public List<string> ApiKeys { get; set; } = new();

    public static string GeneratePartitionKey()
    {
        return "settings";
    }

    public static ProductTrackboxSettingsEntity Create(IProductTrackboxSettings src)
    {
        return new ProductTrackboxSettingsEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = "trackbox",
            ApiKeys = src.ApiKeys,
        };
    }
}