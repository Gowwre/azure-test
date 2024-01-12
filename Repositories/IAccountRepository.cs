using BusinessObjects.Entities;

namespace Repositories;

public interface IAccountRepository
{
    Task<List<Subject>> GetSubjectsAsync();
    Task<List<Account>> GetAccountsAsync();
}