using AutoMapper;
using Fullstack.Application.Dtos;
using Fullstack.Domain;

namespace fullstack.Application.Helpers
{
    public class FullstackProfile : Profile
    {
        
        public FullstackProfile(){
            CreateMap<Evento, EventoDto>().ReverseMap();
        }
    }
}