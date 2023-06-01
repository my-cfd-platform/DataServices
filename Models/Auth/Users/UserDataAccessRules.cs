using System.ComponentModel;

namespace DataServices.Models.Auth.Users
{
    [Flags]
    public enum UserDataAccessRules
    {
        [Description("Assigned to user")] AssignedToUser = 0,
        [Description("Assigned to team members")] AssignedToTeamMembers = 1,
        [Description("Without assignment")] WithoutAssignment = 2,
        [Description("With assignment")] WithAssignment = 4
    }
}