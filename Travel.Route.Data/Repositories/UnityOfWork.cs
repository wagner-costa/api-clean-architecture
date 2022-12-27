using Travel.Route.Data.Context;
using Travel.Route.Domain.Contracts.Repositories;

namespace Travel.Route.Data.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly AppDbContext _context;

        public UnityOfWork(AppDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void BeginTransaction()
        {
            _context?.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_context?.Database?.CurrentTransaction != null)
            {
                _context?.Database.CommitTransaction();
            }
        }

        public void RollBackTransaction()
        {
            if (_context?.Database?.CurrentTransaction != null)
            {
                _context?.Database.RollbackTransaction();
            }
        }
    }
}
