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
        public Int64 Numero { get; set; }
        public Decimal ValorInicial { get; set; }
        public SituacaoCaixa Situacao { get; set; }
        public ICollection<FluxoCaixa> FluxosCaixa { get; set; } = new List<FluxoCaixa>();
    }
}
