using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore; 
using Persistence.Data;

namespace Persistence.Repositories
{
    class GenericRepository<TEntity, Tkey>(StoreDbContext _storeDbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) => await _storeDbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _storeDbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Tkey id) => await _storeDbContext.Set<TEntity>().FindAsync(id);
        public void DeleteAsync(TEntity entity) => _storeDbContext.Set<TEntity>().Remove(entity);
        public void UpdateAsync(TEntity entity) => _storeDbContext.Set<TEntity>().Update(entity);

            #region With Specifiction
            public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification)
            {
                var query = await SpecificationsEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), specification).ToListAsync();
                return query;
            }

            public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification)
            {
                var query = await SpecificationsEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
                return query;
            }

            public async Task<int> GetCountAsync(ISpecification<TEntity, Tkey> specification)
            {
                var query = SpecificationsEvaluator.CreateQuery(_storeDbContext.Set<TEntity>(), specification);
                return await query.CountAsync();
            }
            #endregion
    }
}
 