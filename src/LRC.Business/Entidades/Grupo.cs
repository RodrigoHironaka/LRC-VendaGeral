using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Grupo : Entidade
    {
        public string? Nome { get; set; }
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public ICollection<Subgrupo> Subgrupos { get; set; } = new List<Subgrupo>();
    }
}
