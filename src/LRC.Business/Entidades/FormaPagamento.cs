using LRC.Business.Entidades.Enums;

namespace LRC.Business.Entidades
{
    public class FormaPagamento : Entidade
    {
        public string? Nome { get; set; }
        public Int32 QtdParcelamento { get; set; } 
        public Int32 PeridoParcelamento { get; set; }
        public Situacao Situacao { get; set; }
        public FluxoCaixa? FluxoCaixa { get; set; }
        public Parcela? Parcela { get; set; }
    }
}
