using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class FluxoCaixa : Entidade
    {
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public DebitoCredito DebitoCredito { get; set; } //Débito = Pagamento | Crédito = Recebimento
        
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento? FormaPagamento { get; set; }

        public Guid CaixaId { get; set; }
        public Caixa? Caixa { get; set; }
    }
}
