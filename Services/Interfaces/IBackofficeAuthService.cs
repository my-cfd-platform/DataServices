using DataServices.Models.Teams;
using DataServices.MyNoSql.Interfaces;
using MyCrm.Auth.Common.Roles;
using MyCrm.Auth.Common.Users;

namespace DataServices.Services.Interfaces;

public interface IBackofficeAuthService
{
    #region Users

    Task<IEnumerable<BackOfficeUserModel>> GetAllUsersAsync();
    ValueTask<IBackOfficeUser?> GetUserByIdAsync(string boUserId);
    ValueTask<string> GetUserByCertAliasAsync(string certAlias);
    Task UpdateUserAsync(IBackOfficeUser user);

    #endregion

    #region Roles

    Task<IEnumerable<BackofficeRoleModel>> GetAllRolesAsync();

    Task AddUpdateRoleAsync(BackofficeRoleModel backOfficeRole);
    Task<BackofficeRoleModel> GetRoleByIdAsync(string roleId);

    #endregion

    #region Teams

    Task AddUpdateTeamAsync(IBackofficeTeam backOfficeRole);
    Task<IEnumerable<IBackofficeTeam>> GetAllTeamsAsync();
    Task<IBackofficeTeam> GetTeamByIdAsync(string id);
    Task DeleteTeamAsync(string key);

    #endregion
}