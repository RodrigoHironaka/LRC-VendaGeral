using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Caixa : Entidade
    {
        public SituacaoCaixa Situacao { get; set; }
        public Int64 Numero { get; set; }
        public IEnumerable<FluxoCaixa>? FluxosCaixa { get; set; }
    }
}
