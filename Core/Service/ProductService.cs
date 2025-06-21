using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Service
{
    public class ProductService (IUnitOfWork _unitOfWork,IMapper _mapper ) : IProductService
    {
   

        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Specfiction =new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(Specfiction);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(products);
            var ProductCount= products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.GetCountAsync(CountSpec);
            return new PaginatedResult<ProductDTo>(queryParams.PageIndex,ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<BrandDTo>> GetAllBrandAsyc()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTo>>(Brands);
            return BrandsDto;
        }

        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(Types);
            return TypesDto;
        }

        public async Task<ProductDTo> GetProductByIdAsync(int Id)
        {
            var Specfiction = new ProductWithBrandAndTypeSpecifications(Id);

            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specfiction);
            return _mapper.Map<Product, ProductDTo>( Product);
        }

    }
}
 