﻿using AutoMapper;
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
            CreateMap<Colaborador, ColaboradorVM>().ReverseMap();
            CreateMap<Entregador, EntregadorVM>().ReverseMap();
            CreateMap<Fornecedor, FornecedorVM>().ReverseMap();
            CreateMap<FormaPagamento, FormaPagamentoVM>().ReverseMap();
            CreateMap<ContaPagar, ContaPagarVM>().ReverseMap();
            CreateMap<ContaReceber, ContaReceberVM>().ReverseMap();
            CreateMap<Caixa, CaixaVM>().ReverseMap();
            CreateMap<FluxoCaixa, FluxoCaixaVM>().ReverseMap();
        }
    }
}
