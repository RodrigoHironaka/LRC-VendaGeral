using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Servicos
{
    public class ContaPagarService : BaseService, IContaPagarService
    {
        private readonly IContaPagarRepository _contaPagarRepository;

        public ContaPagarService(IContaPagarRepository contaPagarRepository, INotificador notificador) : base(notificador)
        {
            _contaPagarRepository = contaPagarRepository;
        }

        public async Task<ContaPagar> ObterPorId(Guid id)
        {
            return await _contaPagarRepository.ObterPorId(id);
        }

        public async Task<List<ContaPagar>> ObterTodos()
        {
            return await _contaPagarRepository.ObterTodos();
        }

        public async Task Adicionar(ContaPagar entity)
        {
            if (!ExecutarValidacao(new ContaPagarValidation(), entity)) return;
            await _contaPagarRepository.Adicionar(entity);
        }

        public async Task Atualizar(ContaPagar entity)
        {
            if (!ExecutarValidacao(new ContaPagarValidation(), entity)) return;
            await _contaPagarRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _contaPagarRepository.Remover(id);
        }

        public async Task<IEnumerable<ContaPagar>> Buscar(Expression<Func<ContaPagar, bool>> predicate)
        {
            return await _contaPagarRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _contaPagarRepository?.Dispose();
        }
    }
}
