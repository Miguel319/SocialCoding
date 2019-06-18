using System.Linq;
using AutoMapper;
using SocialCoding.API.Dtos;
using SocialCoding.API.Models;

namespace SocialCoding.API.Helpers {
    public class AutoMapperP : Profile {
        public AutoMapperP () {
            CreateMap<Usuario, UsuarioListaDto> ()
                .ForMember (dest => dest.ImagenUrl, opt =>
                    opt.MapFrom (src => src.Imagenes.FirstOrDefault (i => i.DePerfil).Url))
                .ForMember (x => x.Edad, opt => opt.ResolveUsing (d => d.FechaNacimiento.CalcularEdad ()));
            CreateMap<Usuario, UsuarioDetallesDto> ()
                .ForMember (dest => dest.ImagenUrl, opt =>
                    opt.MapFrom (src => src.Imagenes.FirstOrDefault (i => i.DePerfil).Url))
                .ForMember (x => x.Edad, opt => opt.ResolveUsing (d => d.FechaNacimiento.CalcularEdad ()));

            CreateMap<Imagen, ImagenDetallesDto> ();
            CreateMap<UsuarioEdicionDto, Usuario> ();
        }
    }
}