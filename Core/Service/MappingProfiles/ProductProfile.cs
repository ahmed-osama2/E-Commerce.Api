using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductDTo>()
                .ForMember(dist => dist.BrandName, Options => Options.MapFrom(Srs => Srs.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, Options => Options.MapFrom(Srs => Srs.ProductType.Name))
                //.ForMember(dist => dist.PictureUrl,  Options => Options.MapFrom<PictureUrlResolver>());
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());



            CreateMap<ProductType, TypeDTo>();
            CreateMap<ProductBrand, BrandDTo>();
        }

    }
}
