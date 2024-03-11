using DataServices.MyNoSql.Interfaces.ProductSettings;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace DataServices.MyNoSql.Models.ProductSettings;

public class ProductRecaptchaSettingsEntity : MyNoSqlDbEntity, IProductRecaptchaSettings
{
    public string Id => RowKey = "recaptcha";
    [JsonProperty("disabled")]
	public bool Disabled { get; set; }
    [JsonProperty("public_key")]
    public string PublicKey { get; set; } = string.Empty;
    [JsonProperty("secret_key")]
	public string SecretKey { get; set; } = string.Empty;
	[JsonProperty("score_to_verify")]
	public double ScoreToVerify { get; set; }

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
            Disabled = src.Disabled,
            PublicKey = src.PublicKey,
            SecretKey = src.SecretKey,
            ScoreToVerify = src.ScoreToVerify,
        };
    }
}