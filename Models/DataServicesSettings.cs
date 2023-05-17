// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace DataServices.Models;

public class DataServicesSettings
{
    public string TraderCredentialsFlowsGrpcUrl { get; set; } = null!;
    public string ReportGrpcUrl { get; set; } = null!;
    public string AccountsManagerGrpcUrl { get; set; } = null!;
    public string MyNoSqlServerWriterUrl { get; set; } = null!;
    public string MyNoSqlServerReaderTcp { get; set; } = null!;
    public string AuthGrpcUrl { get; set; } = null!;
    public string TradeLogGrpcServiceUrl { get; set; } = null!;
    public string PersonalDataGrpcServiceUrl { get; set; } = null!;
    public string StatusGrpcServiceUrl { get; set; } = null!;
    public string KeyValueGrpcServiceUrl { get; set; } = null!;
    public string WriteDbConnectionString { get; set; } = null!;
    public string ReadDbConnectionString { get; set; } = null!;
}