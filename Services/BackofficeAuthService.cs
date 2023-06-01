using DataServices.Extensions;
using DataServices.Models;
using DataServices.Models.Auth.Roles;
using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Models;
using DataServices.MyNoSql.Providers;
using DataServices.Services.Interfaces;
using MyNoSqlServer.DataReader;

namespace DataServices.Services;

public class BackofficeAuthService : IBackofficeAuthService
{
    private readonly IRepository<IBackofficeUser> _usersRepository;
    private readonly ICache<IBackofficeUser> _usersCache;

    private readonly IRepository<IBackofficeRole> _rolesRepository;
    private readonly ICache<IBackofficeRole> _rolesCache;

    private readonly IRepository<IBackofficeTeam> _teamsRepository;
    private readonly ICache<IBackofficeTeam> _teamsCache;

    private readonly IRepository<IBackofficeOffice> _officesRepository;
    private readonly ICache<IBackofficeOffice> _officesCache;

    private readonly IRepository<IBackofficeAutoOwner> _autoOwnersRepository;
    private readonly ICache<IBackofficeAutoOwner> _autoOwnersCache;

    public BackofficeAuthService(DataServicesSettings settings, IMyNoSqlSubscriber tcpConnection)
    {
        _usersRepository = new BackOfficeUsersRepository(settings.MyNoSqlServerWriterUrl);
        _usersCache = new BackOfficeUsersCache(tcpConnection);

        _rolesRepository = new BackOfficeRolesRepository(settings.MyNoSqlServerWriterUrl);
        _rolesCache = new BackOfficeRolesCache(tcpConnection);

        _teamsRepository = new BackOfficeTeamsRepository(settings.MyNoSqlServerWriterUrl);
        _teamsCache = new BackOfficeTeamsCache(tcpConnection);

        _officesRepository = new BackOfficeOfficesRepository(settings.MyNoSqlServerWriterUrl);
        _officesCache = new BackOfficeOfficesCache(tcpConnection);

        _autoOwnersCache = new BackOfficeAutoOwnersCache(tcpConnection);
        _autoOwnersRepository = new BackOfficeAutoOwnersRepository(settings.MyNoSqlServerWriterUrl);

    }

    #region Users

    public IEnumerable<BackOfficeUserModel> GetAllUsers()
    {
        var users = _usersCache.GetAll();

        var roles = GetAllRoles();

        var result = users
            .Select(m => BackofficeUserMyNoSqlEntity.ToDomain(m, roles));
        return result;
    }

    public IBackOfficeUser? GetUserById(string id)
    {
        var user = _usersCache.Get(id);
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (user == null)
        {
            return null;
        }

        var roles = GetAllRoles();
        return BackofficeUserMyNoSqlEntity.ToDomain(user, roles);
    }

    public string GetUserByCertAlias(string certAlias)
    {
        var users = GetAllUsers();
        return users.FirstOrDefault(u => u.CertAliases.Contains(certAlias))?.Id!;
    }

    public async Task AddUpdateUserAsync(IBackOfficeUser user)
    {
        await _usersRepository.UpdateAsync(BackofficeUserMyNoSqlEntity.Create(user));
    }

    #endregion

    #region Roles

    public IEnumerable<BackofficeRoleModel> GetAllRoles()
    {
        var roles = _rolesCache.GetAll().Select(BackofficeRoleMyNoSqlEntity.ToDomain);
        return roles;
    }

    public BackofficeRoleModel GetRoleById(string id)
    {
        var role = _rolesCache.Get(id);

        return BackofficeRoleMyNoSqlEntity.ToDomain(role);
    }

    public async Task AddUpdateRoleAsync(BackofficeRoleModel backOfficeRole)
    {
        await _rolesRepository.UpdateAsync(BackofficeRoleMyNoSqlEntity.Create(backOfficeRole));
    }

    #endregion

    #region Teams

    public IEnumerable<IBackofficeTeam> GetAllTeams()
    {
        return _teamsCache.GetAll();
    }

    public IBackofficeTeam GetTeamById(string id)
    {
        return _teamsCache.Get(id);
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

    #region Offices

    public IEnumerable<IBackofficeOffice> GetAllOffices()
    {
        return _officesCache.GetAll();
    }

    public IBackofficeOffice GetOfficeById(string id)
    {
        return id.IsNullOrEmpty() ? null! : _officesCache.Get(id);
    }

    public string GetOfficeNameById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return "<No office>";
        }
        var office = GetOfficeById(id);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return office == null ? "<Not Found>" : office.Name;
    }

    public async Task AddUpdateOfficeAsync(IBackofficeOffice office)
    {
        await _officesRepository.UpdateAsync(office);
    }

    #endregion

    #region AutoOwner

    public IEnumerable<IBackofficeAutoOwner> GetAllAutoOwners()
    {
        return _autoOwnersCache.GetAll();
    }

    public async Task DeleteAutoOwnerAsync(string key)
    {
        await _autoOwnersRepository.DeleteAsync(key);
    }

    public async Task AddUpdateAutoOwnerAsync(IBackofficeAutoOwner owner)
    {
        await _autoOwnersRepository.UpdateAsync(owner);
    }

    #endregion
}