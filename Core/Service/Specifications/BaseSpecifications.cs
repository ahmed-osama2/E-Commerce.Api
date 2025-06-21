using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include

        public List<Expression<Func<TEntity, object>>> IncludExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> expression) => IncludExpressions.Add(expression);
        #endregion

        #region OrderBy

        public Expression<Func<TEntity, object>>? OrderByAsc { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }
        protected void AddOrderByAsc(Expression<Func<TEntity, object>> expression) => OrderByAsc = expression;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression) => OrderByDesc = expression;

        #endregion

        #region Pagination
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagingEnabled { get; set; }

        protected void ApplyPaging(int PageSize, int PageIndex)
        {
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
            IsPagingEnabled = true;
        }
        #endregion


    }
}
