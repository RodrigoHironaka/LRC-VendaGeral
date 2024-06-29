﻿using LRC.Business.Entidades.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LRC.App.ViewModels
{
    public class ClienteVM : PessoaVM
    {
        public string? Logradouro { get; set; }

        [DisplayName("Nº")]
        public string? Numero { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        public string? Referencia { get; set; }

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
