using MovieStore.BAL.Interfaces;
using MovieStore.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MovieStoreDbContext _dbContext;

        public UnitOfWork(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();

        public void Dispose()
            => _dbContext.Dispose();
    }
}
