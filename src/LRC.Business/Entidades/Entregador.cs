using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Entregador : Pessoa
    {
        public Situacao Situacao { get; set; } = Situacao.Ativo;
        public TipoVeiculo TipoVeiculo { get; set; } = TipoVeiculo.Moto;
        public string? Placa { get; set; }
    }
}
