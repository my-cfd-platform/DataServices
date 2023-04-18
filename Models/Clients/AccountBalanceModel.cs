using DataServices.Extensions;
using ReportGrpc;

namespace DataServices.Models.Clients;

public class AccountBalanceModel
{
    public double Amount { get; set; }
    public double BalanceAfterOperation { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public static AccountBalanceModel FromGrpc(ReportFlowsOperationHistoryUiItem src)
    {
        return new AccountBalanceModel
        {
            Amount = src.Amount,
            BalanceAfterOperation = src.BalanceAfterOperation,
            Description = src.Description,
            CreatedAt = src.CreatedAt.EpochMicToDateTime()
        };
    }
}

