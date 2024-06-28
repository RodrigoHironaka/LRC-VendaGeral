using LRC.Business.Entidades.Componentes;

namespace LRC.Business.Entidades
{
    public class LogAlteracao : Entidade
    {
        public String? Chave { get; set; }
        public String? Historico { get; set; }
    }
}
