using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;

namespace LRC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Grupo, GrupoVM>().ReverseMap();
        }
    }
}
