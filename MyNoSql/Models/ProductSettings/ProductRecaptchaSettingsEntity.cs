using System.Text.Json.Serialization;
using DataServices.MyNoSql.Interfaces.ProductSettings;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace DataServices.MyNoSql.Models.ProductSettings;

public class ProductRecaptchaSettingsEntity : MyNoSqlDbEntity, IProductRecaptchaSettings
{
    public new string RowKey = "recaptcha";
    public string Id => RowKey;

    [JsonProperty("public_key")]
    public string PublicKey { get; set; }

    public static string GeneratePartitionKey()
    {
        return "settings";
    }

    public static ProductRecaptchaSettingsEntity Create(IProductRecaptchaSettings src)
    {
        return new ProductRecaptchaSettingsEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = "recaptcha",
            PublicKey = src.PublicKey,
        };
    }
}