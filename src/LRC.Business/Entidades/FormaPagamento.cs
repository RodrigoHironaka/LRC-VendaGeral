using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;

namespace LRC.Business.Entidades
{
    public class FormaPagamento : Entidade
    {
        public string? Nome { get; set; }
        public Int32 QtdParcelamento { get; set; } 
        public Int32 PeriodoParcelamento { get; set; }
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public ICollection<FluxoCaixa> FluxosCaixa { get; set; } = new List<FluxoCaixa>();
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
