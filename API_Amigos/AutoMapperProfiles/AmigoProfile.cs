using API_Amigos.Models;
using API_Amigos.Resources.AmigoResource;
using AutoMapper;
using Domain.Amigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Amigos.AutoMapperProfiles
{
    public class AmigoProfile : Profile
    {
        public AmigoProfile()
        {
            CreateMap<AmigoResquest, Amigo>();
            CreateMap<Amigo, AmigoResponse>();
            CreateMap<Amigo, AmigoResponseWithAmizades>();
        }
    }
}
