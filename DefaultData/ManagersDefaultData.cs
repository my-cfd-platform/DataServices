namespace DataServices.DefaultData;

public static class ManagersDefaultData
{
    public static readonly IReadOnlyDictionary<string, string> Data =
        new Dictionary<string, string>
        {
            {"notSet", "Not Set"},
            {ManagerType.Retention, "Retention Manager"},
            {ManagerType.Conversion, "Conversion Manager" }
        };
}