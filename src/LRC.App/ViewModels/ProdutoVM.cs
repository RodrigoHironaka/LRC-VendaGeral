﻿using LRC.Business.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace LRC.App.ViewModels
{
    public class ProdutoVM
    {
       

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Moeda]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Grupo/SubGrupo")]
        public Guid SubGrupoId { get; set; }
        public SubGrupoVM? SubGrupo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Situação")]
        public Situacao? Situacao { get; set; } = Business.Entidades.Enums.Situacao.Ativo;

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("UN")]
        public UnidadeMedida? UnidadeMedida { get; set; }

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

        public IEnumerable<SubGrupoVM>? SubGrupos { get; set; }

        
    }
}
