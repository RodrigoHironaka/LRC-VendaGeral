using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class PedidoItem : Entidade
    {
        public UnidadeMedida UnidadeMedida { get; set; } = UnidadeMedida.UN;
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
    }
}
