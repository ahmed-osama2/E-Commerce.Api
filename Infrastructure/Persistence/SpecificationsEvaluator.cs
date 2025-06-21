using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.IncludExpressions != null && specification.IncludExpressions.Count > 0)
            {
                //foreach (var includeExpression in specification.IncludExpressions)
                //{
                //    query = query.Include(includeExpression);
                //}

                query = specification.IncludExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }
            if (specification.OrderByAsc != null)
            {
                query = query.OrderBy(specification.OrderByAsc);
            }
            else if (specification.OrderByDesc != null)
            {
                query = query.OrderByDescending(specification.OrderByDesc);
            }
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }


            return query;
        }
    }
}
