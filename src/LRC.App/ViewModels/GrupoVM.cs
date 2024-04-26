using LRC.Business.Entidades.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LRC.App.ViewModels
{
    public class GrupoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")]
        public Situacao? Situacao { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataAlteracao { get; set; }
    }
}
