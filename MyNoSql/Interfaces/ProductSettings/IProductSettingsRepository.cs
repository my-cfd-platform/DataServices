namespace DataServices.MyNoSql.Interfaces.ProductSettings;

public interface IProductSettingsRepository
{
    public Task<IProductBrandSettings> GetBrandSettingsAsync();
    public void SetBrandSettingsAsync(IProductBrandSettings brandSettings);
    public Task<IProductRecaptchaSettings> GetRecaptchaSettingsAsync();
    public Task<IProductTrackboxSettings> GetTrackboxSettingsAsync();
}