using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Interfaces.Authentication;

namespace DataServices.Services.Interfaces;

public interface IAdminAuthService
{
    #region Users

    Task<IEnumerable<AdminUserModel>> GetAllUsers();
    Task<IAdminUser?> GetUserById(string id);
    Task<string> GetUserByCertAlias(string certAlias);
    Task AddUpdateUserAsync(IAdminUser user);

    #endregion
}