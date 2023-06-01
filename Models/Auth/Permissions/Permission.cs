namespace DataServices.Models.Auth.Permissions
{
    public class Permission
    {
        public PermissionResource Resource { get; private set; }
        public PermissionActions Actions { get; private set; }

        public Permission(PermissionResource resource, PermissionActions actions)
        {
            Resource = resource;
            Actions = actions;
        }

        public bool HasViewAction()
        {
            return Actions.HasFlag(PermissionActions.View);
        }
        
        public bool HasEditAction()
        {
            return Actions.HasFlag(PermissionActions.Edit);
        }
        
        public bool HasDeleteAction()
        {
            return Actions.HasFlag(PermissionActions.Delete);
        }
        
        public bool HasAddAction()
        {
            return Actions.HasFlag(PermissionActions.Add);
        }
        
        public bool HasDownloadAction()
        {
            return Actions.HasFlag(PermissionActions.Download);
        }
        
        public bool HasImportAction()
        {
            return Actions.HasFlag(PermissionActions.Import);
        }
    }
}