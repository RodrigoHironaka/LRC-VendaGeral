using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(x => x.Numero)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.TipoPedido)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.DataPedido)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.Situacao)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.Total)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.ClienteId)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");

            RuleFor(x => x.FormaPagamentoId)
           .NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
