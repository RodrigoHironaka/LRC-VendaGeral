using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Cliente : Pessoa
    {
        public  string? Responsaveis { get; set; }
        public Situacao Situacao { get; set; }

        public ContaReceber? ContaReceber { get; set; }
        public ContaPagar? ContaPagar { get; set; }
    }
}
