using System.Collections.Concurrent;
using DataServices.MyNoSql.Interfaces;

namespace DataServices.Models.Backoffice;

public class StatusModel : IStatus
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string Type { get; set; }

    public IEnumerable<string> Labels { get; set; } = new List<string>();
}