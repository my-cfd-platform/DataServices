using DataServices.Models;
using DataServices.Models.Auth.Users;
using DataServices.MyNoSql.Interfaces;
using DataServices.MyNoSql.Interfaces.Authentication;
using DataServices.MyNoSql.Models.Authentication;
using DataServices.MyNoSql.Providers.Authentication;
using DataServices.Services.Interfaces;

namespace DataServices.Services;

public class AdminAuthService : IAdminAuthService
{
    private readonly IRepository<IAdminUser> _usersRepository;
    //private readonly ICache<IBackofficeUser> _usersCache;
    private bool _usersInitialised;

    public AdminAuthService(DataServicesSettings settings)
    {
        _usersRepository = new AdminUsersRepository(settings.MyNoSqlServerWriterUrl);
        //_usersCache = new BackOfficeUsersCache(tcpConnection);
    }

    #region Users

    public async Task InitUsers()
    {
        if (_usersInitialised)
            return;
        if (await _usersRepository.GetCountAsync() > 0)
        {
            _usersInitialised = true;
            return;
        };

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (await _usersRepository.GetAsync("admin") is null)
        {
            await AddUpdateUserAsync(new AdminUserModel
            {
                Id = "admin",
                PersonalName = "Global Admin",
                Registered = DateTime.Now,
                IsBlocked = false,
            });
        }

        _usersInitialised = true;
    }

    public async Task<IEnumerable<AdminUserModel>> GetAllUsers()
    {
        await InitUsers();
        var users = await _usersRepository.GetAllAsync(AdminUserMyNoSqlEntity.GeneratePartitionKey());
        var result = users
            .Select(AdminUserMyNoSqlEntity.ToDomain);
        return result;
    }

    public async Task<IAdminUser?> GetUserById(string id)
    {
        await InitUsers();
        var user = await _usersRepository.GetAsync(id);

        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        return user ?? null;
    }

    public async Task<string> GetUserByCertAlias(string certAlias)
    {
        var users = await GetAllUsers();
        return users.FirstOrDefault(u => u.CertAliases.Contains(certAlias))?.Id!;
    }

    public async Task AddUpdateUserAsync(IAdminUser user)
    {
        await _usersRepository.UpdateAsync(AdminUserMyNoSqlEntity.Create(user));
    }

    #endregion
}