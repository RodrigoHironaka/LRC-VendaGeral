using LRC.Business.Entidades.Enums;
using LRC.Business.Entidades;
using System.ComponentModel.DataAnnotations;

namespace LRC.App.ViewModels
{
    public abstract class PessoaVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public DateTime? Nascimento { get; set; }
        public Int64 Documento { get; set; }
        public string? Documento2 { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? Email { get; set; }
        public TipoPessoa TipoPessoa { get; set; } = TipoPessoa.Física;
        public EnderecoVM? Endereco { get; set; }

        public string? DocumentoFormatado
        {
            get
            {
                if (TipoPessoa == TipoPessoa.Física)
                    return Documento.ToString(@"000\.000\.000\-00");
                else
                    return Documento.ToString(@"00\.000\.000\/0000\-00");
            }
        }
    }
}
