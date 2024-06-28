using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Subgrupo : Entidade
    {
        public string? Nome { get; set; }
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Guid GrupoId { get; set; }
        public Grupo? Grupo { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
