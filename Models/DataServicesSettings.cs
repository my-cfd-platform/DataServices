using SimpleTrading.SettingsReader;

namespace DataServices.Models;

public class DataServicesSettings
{
    public string TraderCredentialsFlowsGrpcUrl { get; set; }
    
    public string ReportGrpcUrl { get; set; }
    public string AccountsManagerGrpcUrl { get; set; }

    public string MyNoSqlServerWriterUrl { get; set; }

    public string MyNoSqlServerReaderTcp { get; set; }

    public string AuthGrpcUrl { get; set; }

    public string TradeLogGrpcServiceUrl { get; set; }
}