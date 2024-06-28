using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;

namespace LRC.Business.Entidades
{
    public class Colaborador : Pessoa
    {
        public virtual DateTime? Admissao { get; set; }
        public virtual DateTime? Demissao { get; set; }
        public Situacao Situacao { get; set; } = Situacao.Ativo;
    }
}
