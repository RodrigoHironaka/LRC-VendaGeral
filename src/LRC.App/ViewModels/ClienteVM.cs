using LRC.Business.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LRC.App.ViewModels
{
    public class ClienteVM : PessoaVM
    {
        //[Key]
        //public Guid Id { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //public string? RazaoSocial { get; set; }
        //public string? NomeFantasia { get; set; }
        //public DateTime? Nascimento { get; set; }
        //public Int64 Documento { get; set; }
        //public string? Documento2 { get; set; }
        //public string? Telefone { get; set; }
        //public string? Celular { get; set; }
        //public string? Celular2 { get; set; }
        //public string? Email { get; set; }
        //public TipoPessoa TipoPessoa { get; set; } = TipoPessoa.Física;
        
        public string? Logradouro { get; set; }

        [DisplayName("Nº")]
        public string? Numero { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? Referencia { get; set; }

        //public string? DocumentoFormatado
        //{
        //    get
        //    {
        //        if (TipoPessoa == TipoPessoa.Física)
        //            return Documento.ToString(@"000\.000\.000\-00");
        //        else
        //            return Documento.ToString(@"00\.000\.000\/0000\-00");
        //    }
        //}
        public string? Responsaveis { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")]
        public Situacao Situacao { get; set; } = Situacao.Ativo;

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
