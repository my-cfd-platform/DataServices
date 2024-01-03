using DataServices.Models.Auth.Roles;
using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Interfaces;

namespace DataServices.Services.Interfaces;

public interface IBackofficeAuthService
{
    #region Users

    IEnumerable<BackOfficeUserModel> GetAllUsers();
    IBackOfficeUser? GetUserById(string id);
    string GetUserByCertAlias(string certAlias);
    Task AddUpdateUserAsync(IBackOfficeUser user);

    #endregion

    #region Roles
    IEnumerable<BackofficeRoleModel> GetAllRoles();

    Task AddUpdateRoleAsync(BackofficeRoleModel backOfficeRole);
    BackofficeRoleModel GetRoleById(string id);
    Task DeleteRoleAsync(string key);
    #endregion

    #region Teams

    Task AddUpdateTeamAsync(IBackofficeTeam backOfficeRole);
    IEnumerable<IBackofficeTeam> GetAllTeams();
    IBackofficeTeam GetTeamById(string id);
    Task DeleteTeamAsync(string key);

    #endregion

    #region Offices

    IEnumerable<IBackofficeOffice> GetAllOffices();
    IBackofficeOffice GetOfficeById(string id);
    string GetOfficeNameById(string id);
    Task AddUpdateOfficeAsync(IBackofficeOffice office);

    #endregion

    #region AutoOwner

    IEnumerable<IBackofficeAutoOwner> GetAllAutoOwners();
    Task DeleteAutoOwnerAsync(string key);
    Task AddUpdateAutoOwnerAsync(IBackofficeAutoOwner owner);

    #endregion
}