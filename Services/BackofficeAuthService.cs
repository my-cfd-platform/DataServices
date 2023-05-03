using DataServices.Extensions;
using DataServices.Models;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using DataServices.MyNoSql.Providers;
using DataServices.Services.Interfaces;
using MyCrm.Auth.Common.Roles;
using MyCrm.Auth.Common.Users;

namespace DataServices.Services;

public class BackofficeAuthService : IBackofficeAuthService
{
    private readonly IRepository<IBackofficeUser> _usersRepository;

    private readonly IRepository<IBackofficeRole> _rolesRepository;
    private readonly IRepository<IBackofficeTeam> _teamsRepository;
    //private readonly ICache<IBackofficeUser> _usersCache;
    public BackofficeAuthService(DataServicesSettings settings)
    {
        if (settings.MyNoSqlServerWriterUrl.IsNotNullOrEmpty())
        {
            _usersRepository = new BackOfficeUsersRepository(settings.MyNoSqlServerWriterUrl);
            _rolesRepository = new BackOfficeRolesRepository(settings.MyNoSqlServerWriterUrl);
            _teamsRepository = new BackOfficeTeamsRepository(settings.MyNoSqlServerWriterUrl);
        }
    }

    #region Users

    public async Task<IEnumerable<BackOfficeUserModel>> GetAllUsersAsync()
    {
        var models = await _usersRepository.GetAllAsync();

        var roles = await GetAllRolesAsync();

        var result = models
            .Select(m => BackofficeUserMyNoSqlEntity.ToDomain(m, roles));
        return result;
    }

    public async ValueTask<IBackOfficeUser?> GetUserByIdAsync(string boUserId)
    {
        var user = await _usersRepository.GetAsync(boUserId);
        if (user == null)
        {
            return null;
        }

        var roles = await GetAllRolesAsync();
        return BackofficeUserMyNoSqlEntity.ToDomain(user, roles);
    }

    public async ValueTask<string> GetUserByCertAliasAsync(string certAlias)
    {
        //todo load users via cache?
        var users = await GetAllUsersAsync();
        return users.FirstOrDefault(u => u.CertAliases.Contains(certAlias))?.Id;
    }

    public async Task UpdateUserAsync(IBackOfficeUser user)
    {
        await _usersRepository.UpdateAsync(BackofficeUserMyNoSqlEntity.Create(user));
    }

    #endregion

    #region Roles

    public async Task<IEnumerable<BackofficeRoleModel>> GetAllRolesAsync()
    {
        //todo load roles via cache?
        return (await _rolesRepository.GetAllAsync()).Select(BackofficeRoleMyNoSqlEntity.ToDomain);
    }

    public async Task<BackofficeRoleModel> GetRoleByIdAsync(string roleId)
    {
        var role = await _rolesRepository.GetAsync(roleId);
        //todo load roles via cache?
        return BackofficeRoleMyNoSqlEntity.ToDomain(role);
    }

    public async Task AddUpdateRoleAsync(BackofficeRoleModel backOfficeRole)
    {
        await _rolesRepository.UpdateAsync(BackofficeRoleMyNoSqlEntity.Create(backOfficeRole));
    }

    #endregion

    #region Teams

    public async Task<IBackofficeTeam> GetTeamByIdAsync(string id)
    {
        return await _teamsRepository.GetAsync(id);
    }

    public async Task<IEnumerable<IBackofficeTeam>> GetAllTeamsAsync()
    {
        return await _teamsRepository.GetAllAsync();
    }

    public async Task AddUpdateTeamAsync(IBackofficeTeam backOfficeRole)
    {
        await _teamsRepository.UpdateAsync(backOfficeRole);
    }

    public async Task DeleteTeamAsync(string key)
    {
        await _teamsRepository.DeleteAsync(key);
    }

    #endregion
}