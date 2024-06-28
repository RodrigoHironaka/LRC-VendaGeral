using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class FluxoCaixaValidation : AbstractValidator<FluxoCaixa>
    {
        public FluxoCaixaValidation()
        {
            RuleFor(x => x.Valor)
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.Data)
           .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.DebitoCredito)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.FormaPagamentoId)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.CaixaId)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
