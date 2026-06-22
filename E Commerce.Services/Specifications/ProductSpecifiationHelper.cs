using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public static class ProductSpecifiationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCritria(ProductQueryParms queryParms)
        {
            return P => (!queryParms.brandId.HasValue || P.BrandId == queryParms.brandId)
                     && (!queryParms.typeId.HasValue || P.TypeId == queryParms.typeId)
                     && (string.IsNullOrEmpty(queryParms.search) || P.Name.Contains(queryParms.search.ToLower()));
        }
    }
}
