using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Pedido : Entidade
    {
        public Int64 Numero { get; set; }
        public TipoPedido TipoPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public SituacaoPedido Situacao { get; set; } = SituacaoPedido.Aberto;
        public decimal Total { get; set; }
        public decimal TrocoPara { get; set; }
        public decimal Troco { get; set; }
        public Int32 Mesa { get; set; }
        public string? MesaPorPessoa { get; set; }
        public string? Observacao { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; } //pode digitar na hora e escolher salvar ou nao
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento? FormaPagamento { get; set; }

        public ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }
}
