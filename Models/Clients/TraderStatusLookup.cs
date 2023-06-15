namespace DataServices.Models.Clients;

public class TraderStatusLookup
{
    public Dictionary<string, string> Statuses { get; set; } = new();
    public Dictionary<string, string> Labels { get; set; } = new();
}