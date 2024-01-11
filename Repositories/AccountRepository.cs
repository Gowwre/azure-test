using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
   private readonly SampleContext _context;

   public AccountRepository()
   {
      _context = new SampleContext();
   }

   public Task<List<Subject>> GetSubjectsAsync()
   {
      var subjects = _context.Subjects.ToListAsync();
      return subjects;
   }
}