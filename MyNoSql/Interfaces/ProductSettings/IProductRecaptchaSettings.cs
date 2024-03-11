namespace DataServices.MyNoSql.Interfaces.ProductSettings;

public interface IProductRecaptchaSettings
{
    public string Id { get; }
    public bool Disabled { get; set; }
    public string PublicKey { get; set; }
    public string SecretKey { get; set; }
    public double ScoreToVerify { get; set; }
}