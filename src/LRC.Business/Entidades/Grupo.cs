using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class Grupo : Entidade
    {
        public string? Nome { get; set; }
        public Situacao? Situacao { get; set; }

        public Subgrupo? Subgrupo { get; set; }
    }
}
