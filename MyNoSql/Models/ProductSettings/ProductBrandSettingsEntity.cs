using DataServices.MyNoSql.Interfaces.ProductSettings;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

namespace DataServices.MyNoSql.Models.ProductSettings;

public class ProductBrandSettingsEntity : MyNoSqlDbEntity, IProductBrandSettings
{
    public string Id => RowKey = "brand";
    [JsonProperty("logoUrl")]
    public string LogoUrl { get; set; } = string.Empty;
    [JsonProperty("policyUrl")]
    public string PolicyUrl { get; set; } = string.Empty;
    [JsonProperty("termsUrl")]
    public string TermsUrl { get; set; } = string.Empty;
    [JsonProperty("withdrawFaqUrl")]
    public string WithdrawFaqUrl { get; set; } = string.Empty;
    [JsonProperty("aboutUrl")]
    public string AboutUrl { get; set; } = string.Empty;
    [JsonProperty("supportUrl")]
    public string SupportUrl { get; set; } = string.Empty;
    [JsonProperty("brandName")]
    public string BrandName { get; set; } = string.Empty;
    [JsonProperty("brandCopyrights")]
    public string BrandCopyrights { get; set; } = string.Empty;
    [JsonProperty("gaAsAccount")]
    public string GaAsAccount { get; set; } = string.Empty;
    [JsonProperty("mixPanelToken")]
    public string MixPanelToken { get; set; } = string.Empty;
    [JsonProperty("faviconUrl")]
    public string FaviconUrl { get; set; } = string.Empty;
    [JsonProperty("androidAppId")]
    public string AndroidAppId { get; set; } = string.Empty;
    [JsonProperty("androidAppLink")]
    public string AndroidAppLink { get; set; } = string.Empty;
    [JsonProperty("iosAppId")]
    public string IosAppId { get; set; } = string.Empty;
    [JsonProperty("iosAppLink")]
    public string IosAppLink { get; set; } = string.Empty;
    [JsonProperty("mobileAppLogo")]
    public string MobileAppLogo { get; set; } = string.Empty;

    public static string GeneratePartitionKey()
    {
        return "settings";
    }

    public static ProductBrandSettingsEntity Create(IProductBrandSettings src)
    {
        return new ProductBrandSettingsEntity
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = "brand",
            LogoUrl = src.LogoUrl,
            PolicyUrl = src.PolicyUrl,
            TermsUrl = src.TermsUrl,
            WithdrawFaqUrl = src.WithdrawFaqUrl,
            AboutUrl = src.AboutUrl,
            SupportUrl = src.SupportUrl,
            BrandName = src.BrandName,
            BrandCopyrights = src.BrandCopyrights,
            GaAsAccount = src.GaAsAccount,
            MixPanelToken = src.MixPanelToken,
            FaviconUrl = src.FaviconUrl,
            AndroidAppId = src.AndroidAppId,
            AndroidAppLink = src.AndroidAppLink,
            IosAppId = src.IosAppId,
            IosAppLink = src.IosAppLink,
            MobileAppLogo = src.MobileAppLogo,
        };
    }
}