namespace DataServices.MyNoSql.Interfaces.ProductSettings;

public interface IProductSettingsRepository
{
    public Task<IProductBrandSettings> GetBrandSettingsAsync();
    public void SetBrandSettingsAsync(IProductBrandSettings settings);
    public Task<IProductRecaptchaSettings> GetRecaptchaSettingsAsync();
    public void SetRecaptchaSettingsAsync(IProductRecaptchaSettings settings);
	public Task<IProductTrackboxSettings> GetTrackboxSettingsAsync();
	public void SetTrackboxSettingsAsync(IProductTrackboxSettings settings);
}