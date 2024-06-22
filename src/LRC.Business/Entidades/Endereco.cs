using LRC.Business.Entidades.Enums;

namespace LRC.Business.Entidades
{
    public class Endereco : Entidade
    {
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public bool Principal { get; set; }
        public Situacao Situacao { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public Guid ColaboradorId { get; set; }
        public Colaborador? Colaborador { get; set; }
        public Guid EntregadorId { get; set; }
        public Entregador? Entregador { get; set; }
    }
}
