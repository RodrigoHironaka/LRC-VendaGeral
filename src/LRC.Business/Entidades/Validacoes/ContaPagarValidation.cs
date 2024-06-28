using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class ContaPagarValidation : AbstractValidator<ContaPagar>
    {
        public ContaPagarValidation()
        {
            RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres");
            
            RuleFor(x => x.DataEmissao)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.Situacao)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
