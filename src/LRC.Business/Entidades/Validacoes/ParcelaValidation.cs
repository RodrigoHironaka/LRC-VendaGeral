using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class ParcelaValidation : AbstractValidator<Parcela>
    {
        public ParcelaValidation()
        {
            RuleFor(x => x.Numero)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ParcelaDe)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres");

            RuleFor(x => x.ValorPago)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.DataVencimento)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ValorReajustado)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ValorAberto)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.SituacaoParcela)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
