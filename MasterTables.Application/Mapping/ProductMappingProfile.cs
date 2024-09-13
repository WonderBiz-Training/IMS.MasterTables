using AutoMapper;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Define the mapping from ProductDto to Product and vice versa
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore CreatedDate in case we handle it separately
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore UpdatedDate similarly

            CreateMap<Product, ProductDto>(); // For reverse mapping, when reading product data
        }
    }
}
