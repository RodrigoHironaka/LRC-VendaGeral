using LRC.Business.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LRC.App.ViewModels
{
    public class ContaPagarVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Emissão")]
        public DateTime DataEmissao { get; set; } = DateTime.Now;

        [DisplayName("Fechamento")]
        public DateTime? DataFechamento { get; set; } = null;

        [DisplayName("Vencimento")]
        public DateTime? DataVencimento { get; set; } = null;

        [Moeda]
        public decimal Valor { get; set; }

        [DisplayName("Nº Doc")]
        public string? NumeroDocumento { get; set; }

        [DisplayName("Observação")]
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")]
        public SituacaoConta Situacao { get; set; } = Business.Entidades.Enums.SituacaoConta.Aberto;

        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        public FornecedorVM? Fornecedor { get; set; }

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
