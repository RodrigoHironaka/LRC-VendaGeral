using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Fornecedor : Pessoa
    {
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public ICollection<ContaPagar> ContasPagar { get; set; } = new List<ContaPagar>();
    }
}
