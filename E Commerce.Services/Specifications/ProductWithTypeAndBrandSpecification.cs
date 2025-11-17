using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        // BrandId Is Not Null
        // TypeId Is Not Null => Criteria => P => P.TypeId == typeId
        // TypeId and BrandId Is Not Null => Criteria => P => P.TypeId == typeId && P.BrandId == brandId
        // TypeId and BrandId Is Null => Criteria => Null
        public ProductWithTypeAndBrandSpecification(ProductQueryParms queryParms)
            : base(ProductSpecifiationHelper.GetProductCritria(queryParms))
        {

            AddIncludes();

            AddSorting(queryParms);

            AddPagination(queryParms);

        }

        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }

        private void AddSorting(ProductQueryParms queryParms)
        {
            switch (queryParms.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Id);
                    break;
            }
        }

        private void AddPagination(ProductQueryParms queryParms) 
        {
            ApplyPagination(queryParms.PageSize, queryParms.PageIndex);
        }
    }
}
