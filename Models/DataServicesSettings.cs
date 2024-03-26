// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
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
    public string CommentGrpcServiceUrl { get; set; } = null!;
    public string KeyValueGrpcServiceUrl { get; set; } = null!;
    public string ManagerAccessGrpcServiceUrl { get; set; } = null!;
    public string KycStatusGrpcServiceUrl { get; set; } = null!;
    public string KycChangeLogsGrpcServiceUrl { get; set; } = null!;
    public string DocumentsGrpcServiceUrl { get; set; } = null!;
    public string WithdrawalsGrpcServiceUrl { get; set; } = null!;
    public string DepositsGrpcServiceUrl { get; set; } = null!;
    public string PositionsManagerServiceUrl { get; set; } = null!;
    public string TraderFtdFlowsGrpcServiceUrl { get; set; } = null!;
    public string InternalReportsFlowsGrpcServiceUrl { get; set; } = null!;

    public DataServicesSettings(object settings)
    {
        var availableFields = this.GetType().GetProperties().ToDictionary(p => p.Name);
        var type = settings.GetType();
        foreach (var declaredField in type.GetProperties())
        {
            if (!availableFields.ContainsKey(declaredField.Name))
                continue;
            var value = declaredField.GetValue(settings);
            availableFields[declaredField.Name].SetValue(this, value);
        }
    }
}