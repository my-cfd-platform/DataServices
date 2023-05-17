using DataServices.Models.Clients;
using DataServices.MyNoSql.Interfaces;

namespace DataServices.Services.Interfaces;

public interface IStatusesService
{
    IEnumerable<IStatus> GetStatuses(string statusType);
    string GetStatusName(string id, string statusType);
    Task<TraderStatusLookup> GetTraderStatusesGrpc(string id);
    Task SetTraderStatus(string id, string status, string label, string statusType);
}