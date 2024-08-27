using LRC.Business.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LRC.App.ViewModels
{
    public class FluxoCaixaVM
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Descrição?")]
        public string? Descricao { get; set; }
        public Decimal Valor { get; set; }
        public DateTime? Data { get; set; } = null;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Débito?")]
        public DebitoCredito DebitoCredito { get; set; } //Débito = Pagamento | Crédito = Recebimento

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Forma Pagamento")]
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamentoVM? FormaPagamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Caixa")]
        public Guid CaixaId { get; set; }
        public CaixaVM? Caixa { get; set; }

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

        public IEnumerable<FormaPagamentoVM>? FormasPagamento { get; set; }
    }
}
