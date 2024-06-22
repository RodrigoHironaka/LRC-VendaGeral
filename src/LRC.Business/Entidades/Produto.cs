using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Produto : Entidade
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Valor { get; set; }
        public Int32? Quantidade { get; set; }
        public Situacao? Situacao { get; set; }

        public Guid SubgrupoId { get; set; }
        public Subgrupo? Subgrupo { get; set; }
    }
}
