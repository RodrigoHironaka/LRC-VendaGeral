using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class PedidoItemValidation : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation()
        {
            RuleFor(x => x.UnidadeMedida)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
            
            RuleFor(x => x.Quantidade)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ValorUnitario)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ValorTotal)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
            
            RuleFor(x => x.PedidoId)
          .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
