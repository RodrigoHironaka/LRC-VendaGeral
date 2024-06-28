using LRC.Business.Entidades.Enums;
using LRC.Business.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace LRC.App.ViewModels
{
    public class EnderecoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nº")]
        public string? Numero { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? Referencia { get; set; }

        public bool Principal { get; set; } = false;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")] 
        public Situacao Situacao { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public Guid ColaboradorId { get; set; }
        public Colaborador? Colaborador { get; set; }
        public Guid EntregadorId { get; set; }
        public Entregador? Entregador { get; set; }


        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataAlteracao { get; set; }
        [ScaffoldColumn(false)]
        public Guid UsuarioCadastroId { get; set; }
        [ScaffoldColumn(false)]
        public Guid UsuarioAlteracaoId { get; set; }
        [ScaffoldColumn(false)]
        public IdentityUser? UsuarioCadastro { get; set; }
        [ScaffoldColumn(false)]
        public IdentityUser? UsuarioAlteracao { get; set; }

        public string _InfoCadastro
        {
            get
            {
                return $"Criação: {UsuarioCadastro?.UserName} - {DataCadastro}";
            }
        }
        public string _InfoAlteracao
        {
            get
            {
                return $"Alteração: {UsuarioAlteracao?.UserName} - {DataAlteracao}";
            }
        }
    }
}
