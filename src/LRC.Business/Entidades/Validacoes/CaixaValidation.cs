using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class CaixaValidation : AbstractValidator<Caixa>
    {
        public CaixaValidation()
        {
            RuleFor(x => x.Situacao).NotNull().WithMessage("O campo {PropertyName} é obrigatório!");
        }
    }
}
