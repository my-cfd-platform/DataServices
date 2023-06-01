namespace DataServices.Models.Auth.Permissions
{
    [Flags]
    public enum PermissionActions
    {
        View = 0,
        Edit = 1 << 0,
        Delete = 1 << 1,
        Add = 1 << 2,
        Download = 1 << 3,
        Import = 1 << 4,
    }
}