using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationEvaluator
    {
        //Create Query - Build Query
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            // _dbContext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand);

            var Query = EntryPoint;
            // _dbContext.Products
            // _dbContext.Products.Include(P => P.ProductType)
            // _dbContext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand);


            if (specifications is not null)
            {
                if (specifications.Critria is not null)
                {
                    // 4 Overload Where()
                    // All Products => IEnumrable
                    // IEnumrable => Get All Products From Database Without Filtration and Filtrate in Code
                    // IQuarble => Get All Products From Database With Filtration
                    Query = Query.Where(specifications.Critria);
                }
                if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Any())
                {
                    //foreach (var includeExpression in specifications.IncludeExpressions)
                    //{
                    //    Query = Query.Include(includeExpression);
                    //}
                    Query = specifications.IncludeExpressions
                        .Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));

                }
                if (specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }
                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                }
                if (specifications.IsPaginated)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }
            // Ahmed, Omar, Aly, Mohamed
            // Ahmed Omar
            // Ahmed Omar Aly
            // Ahmed Omar Aly Mohamed

            return Query;
        }
    }
}
