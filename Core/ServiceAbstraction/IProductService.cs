using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // GetAll Products
        Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams);

        // get Product By Id

        Task<ProductDTo> GetProductByIdAsync(int Id);
        // get All Types
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();
        // get All Brands
        Task<IEnumerable<BrandDTo>> GetAllBrandAsyc();


    }
}
