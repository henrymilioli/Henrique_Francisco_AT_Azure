using API_Amigos.Models;
using AutoMapper;
using Domain.Amigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Amigos.AutoMapperProfiles
{
    public class AmizadeProfile : Profile
    {
        public AmizadeProfile()
        {
            CreateMap<AmizadeRequest, Amizade>();
            CreateMap<Amizade, AmizadeResponse>();
        }
    }
}
