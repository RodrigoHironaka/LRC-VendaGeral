using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;

namespace LRC.Business.Entidades
{
    public class Cliente : Pessoa
    {
        public  string? Responsaveis { get; set; }
        public Situacao Situacao { get; set; } = Situacao.Ativo;


        public ICollection<ContaReceber> ContasReceber { get; set; } = new List<ContaReceber>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
