using AutoMapper;
using Fullstack.Application.Dtos;
using Fullstack.Domain;
using Fullstack.Domain.Identity;
using Fullstack.Persistence.Models;

namespace fullstack.Application.Helpers
{
    public class FullstackProfile : Profile
    {

        public FullstackProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}