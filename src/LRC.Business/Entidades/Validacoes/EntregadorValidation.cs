using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class EntregadorValidation : AbstractValidator<Entregador>
    {
        public EntregadorValidation()
        {
            RuleFor(x => x.RazaoSocial)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres");

            RuleFor(x => x.Situacao)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
