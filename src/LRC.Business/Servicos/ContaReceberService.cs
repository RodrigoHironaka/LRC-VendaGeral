using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using System.Linq.Expressions;

namespace LRC.Business.Servicos
{
    public class ContaReceberService : BaseService, IContaReceberService
    {
        private readonly IContaReceberRepository _contaReceberRepository;

        public ContaReceberService(IContaReceberRepository contaReceberRepository, INotificador notificador) : base(notificador)
        {
            _contaReceberRepository = contaReceberRepository;
        }

        public async Task<ContaReceber> ObterPorId(Guid id)
        {
            return await _contaReceberRepository.ObterPorId(id);
        }

        public async Task<List<ContaReceber>> ObterTodos()
        {
            return await _contaReceberRepository.ObterTodos();
        }

        public async Task Adicionar(ContaReceber entity)
        {
            if (!ExecutarValidacao(new ContaReceberValidation(), entity)) return;
            await _contaReceberRepository.Adicionar(entity);
        }

        public async Task Atualizar(ContaReceber entity)
        {
            if (!ExecutarValidacao(new ContaReceberValidation(), entity)) return;
            await _contaReceberRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _contaReceberRepository.Remover(id);
        }

        public async Task<IEnumerable<ContaReceber>> Buscar(Expression<Func<ContaReceber, bool>> predicate)
        {
            return await _contaReceberRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _contaReceberRepository?.Dispose();
        }
    }
}

