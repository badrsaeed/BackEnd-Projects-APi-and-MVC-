using Microsoft.EntityFrameworkCore;
using MovieStore.BAL.Interfaces;
using MovieStore.BAL.Specifications;
using MovieStore.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BAL.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieStoreDbContext _dbContext;

        public GenericRepository(MovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
            => await _dbContext.AddAsync(item);

        public void Delete(T item)
            => _dbContext.Remove(item);

        public async Task<T> GetAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
         => await _dbContext.Set<T>().ToListAsync();
        public void Update(T item)
         => _dbContext.Set<T>().Update(item);

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
         => await ApplayQuery(spec).ToListAsync();

        public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
         => await ApplayQuery(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplayQuery(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
    }
}
