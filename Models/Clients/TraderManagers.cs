namespace DataServices.Models.Clients;

public class TraderManagers : Dictionary<string, string>
{
    public const string Retention = "Retention";
    public const string Conversion = "Conversion";
    public const string Office = "Office";

    public string? GetManagerId(string managerType)
    {
        TryGetValue(managerType, out var managerId);
        return managerId;
    }
}