using API_Paises.Models.Pais;
using API_Paises.Resources.PaisResource;
using AutoMapper;
using Domain.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Paises.AutoMapperProfiles
{
    public class PaisProfile : Profile
    {
        public PaisProfile()
        {
            CreateMap<PaisRequest, Pais>();
            CreateMap<Pais, PaisResponse>();
            CreateMap<Pais, PaisResponseWithEstados>();
        }
    }
}
