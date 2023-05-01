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

    #endregion

    #region Roles

    Task<IEnumerable<BackofficeRoleModel>> GetAllRolesAsync();

    Task AddRoleAsync(BackofficeRoleModel backOfficeRole);
    Task<BackofficeRoleModel> GetRoleByIdAsync(string roleId);

    #endregion
}