using LRC.Business.Entidades.Componentes;
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
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; } = UnidadeMedida.UN;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Guid SubgrupoId { get; set; }
        public Subgrupo? Subgrupo { get; set; }

        public ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
    }
}
