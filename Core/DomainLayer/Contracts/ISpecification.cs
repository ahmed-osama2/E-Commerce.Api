using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> IncludExpressions { get; }

        public Expression<Func<TEntity, object>>? OrderByAsc { get; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnabled { get; set; }






    }
}
