using AutoMapper;
using TreeStructure.Core.Domain;
using TreeStructure.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeStructure.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Category, CategoryTreeDto>();

            })
            .CreateMapper();
    }
}
