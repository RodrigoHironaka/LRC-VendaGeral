using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public class LogAlteracaoValidation : AbstractValidator<LogAlteracao>
    {
        public LogAlteracaoValidation()
        {
            RuleFor(x => x.Chave)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(x => x.Historico)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
