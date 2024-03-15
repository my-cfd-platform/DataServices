using DataServices.MyNoSql.Enums;

namespace DataServices.MyNoSql.Interfaces.Authentication;

public interface IBackofficeTeam
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> OfficeIds { get; set; }
    //public string TeamLeadId { get; set; }
    public IEnumerable<string> Managers { get; set; }
    public TeamType Type { get; set; }
}