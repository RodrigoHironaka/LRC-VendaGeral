using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres");

            RuleFor(x => x.SubgrupoId)
            .NotEmpty().WithMessage("O campo Grupo/Subgrupo precisa ser fornecido")
            .NotNull().WithMessage("O campo Grupo/Subgrupo é obrigatório!");

            RuleFor(x => x.Situacao)
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
