namespace DataServices.Models.Auth.Permissions
{
    public class PermissionResourceInfo : Attribute
    {
        public string Name { get; private set; }
        public PermissionResource? LinkedTo { get; private set; }
        public PermissionActions[] SupportedLevels { get; set; }
        public bool Demo { get; set; }

        public PermissionResourceInfo(PermissionResource linkedTo, string name)
        {
            LinkedTo = linkedTo;
            Name = name;
            SupportedLevels = new [] {PermissionActions.View};
        }
        
        public PermissionResourceInfo(string name)
        {
            LinkedTo = null;
            Name = name;
            SupportedLevels = new [] {PermissionActions.View};
        }
        
        public PermissionResourceInfo(string name, params PermissionActions[] supportedLevels)
        {
            LinkedTo = null;
            Name = name;
            SupportedLevels = supportedLevels;
        }
        
        public PermissionResourceInfo(PermissionResource linkedTo, string name, params PermissionActions[] supportedLevels)
        {
            LinkedTo = linkedTo;
            Name = name;
            SupportedLevels = supportedLevels;
        }
    }
}