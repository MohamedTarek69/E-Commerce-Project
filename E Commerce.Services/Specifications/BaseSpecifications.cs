using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Includes
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        #endregion

        #region Filterations
        public Expression<Func<TEntity, bool>> Critria { get; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CritriaExpression)
        {
            Critria = CritriaExpression;
        }

        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; } = default!;
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; } = default!;

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        // Total Count = 40
        // PageSize = 10
        // 10 , 10 ,10 ,10
        // PageIndex = 4
        // (4 - 1) * 10 = 30
        public void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }



        #endregion

    }
}
