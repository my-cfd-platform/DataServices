namespace DataServices.MyNoSql.Interfaces.ProductSettings;

public interface IProductBrandSettings
{
    public string Id { get; }
    public string LogoUrl { get; set; }
    public string PolicyUrl { get; set; }
    public string TermsUrl { get; set; }
    public string WithdrawFaqUrl { get; set; }
    public string AboutUrl { get; set; }
    public string SupportUrl { get; set; }
    public string BrandName { get; set; }
    public string BrandCopyrights { get; set; }
    public string GaAsAccount { get; set; }
    public string MixPanelToken { get; set; }
    public string FaviconUrl { get; set; }
    public string AndroidAppId { get; set; }
    public string AndroidAppLink { get; set; }
    public string IosAppId { get; set; }
    public string IosAppLink { get; set; }
    public string MobileAppLogo { get; set; }
}