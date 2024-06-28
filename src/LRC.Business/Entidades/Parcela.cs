using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Parcela : Entidade
    {
        public Int64 Numero { get; set; }
        public string? ParcelaDe { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal JurosPorcentual { get; set; }
        public decimal JurosValor { get; set; }
        public decimal DescontoPorcentual { get; set; }
        public decimal DescontoValor { get; set; }
        public decimal ValorReajustado { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorAberto { get; set; }
        public string? Observacao { get; set; }
        public SituacaoParcela SituacaoParcela { get; set; } = SituacaoParcela.Pendente;

        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento? FormaPagamento { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        public Guid ContaReceberId { get; set; }
        public ContaReceber? ContaReceber { get; set; }

        public Guid ContaPagarId { get; set; }
        public ContaPagar? ContaPagar { get; set; }
    }
}
