using API_Paises.Models.Estado;
using AutoMapper;
using Domain.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Paises.AutoMapperProfiles
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile()
        {
            CreateMap<EstadoRequest, Estado>();
            CreateMap<Estado, EstadoResponse>();
        }
    }
}
