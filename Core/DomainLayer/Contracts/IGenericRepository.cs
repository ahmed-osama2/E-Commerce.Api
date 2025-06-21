﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);

        #region With Specfiction
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification);
        #endregion

        Task<int> GetCountAsync(ISpecification<TEntity, Tkey> specification);

    }
}
