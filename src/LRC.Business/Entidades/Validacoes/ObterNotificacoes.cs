using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades.Validacoes
{
    public static class ObterNotificacoes
    {
        public static String ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entidade
        {
            var notificacoes = new StringBuilder();
            var validator = validacao.Validate(entidade);
            if (validator.IsValid) return string.Empty;

            foreach (var erro in validator.Errors)
            {
                notificacoes.AppendLine(erro.ErrorMessage);
            }
            return notificacoes.ToString();
        }
    }
}
