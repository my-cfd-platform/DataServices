using DataServices.MyNoSql.Enums;
using DataServices.MyNoSql.Interfaces;

namespace DataServices.Models.Backoffice;

public class TeamModel : IBackofficeTeam
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> OfficeIds { get; set; } = new List<string>();
    //public string TeamLeadId { get; set; }
    public IEnumerable<string> Managers { get; set; } = new List<string>();

    public TeamType Type { get; set; }
}