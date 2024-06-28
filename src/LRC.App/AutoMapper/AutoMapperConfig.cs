using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Entidades.Componentes;

namespace LRC.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Grupo, GrupoVM>().ReverseMap();
            CreateMap<Subgrupo, SubGrupoVM>().ReverseMap();
            CreateMap<Produto, ProdutoVM>().ReverseMap();
            CreateMap<Cliente, ClienteVM>().ReverseMap();
            CreateMap<Endereco, EnderecoVM>().ReverseMap();
        }
    }
}
