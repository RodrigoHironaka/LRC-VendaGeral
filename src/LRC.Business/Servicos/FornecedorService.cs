using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using System.Linq.Expressions;

namespace LRC.Business.Servicos
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
            INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task Adicionar(Fornecedor entity)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new FornecedorValidation(), entity)) return;
            await _fornecedorRepository.Adicionar(entity);
        }

        public async Task Atualizar(Fornecedor entity)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), entity)) return;
            await _fornecedorRepository.Atualizar(entity);
        }

        public async Task<IEnumerable<Fornecedor>> Buscar(Expression<Func<Fornecedor, bool>> predicate)
        {
            return await _fornecedorRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }

        public async Task<Fornecedor> ObterPorId(Guid id)
        {
            return await _fornecedorRepository.ObterPorId(id);
        }

        public async Task<List<Fornecedor>> ObterTodos()
        {
            return await _fornecedorRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _fornecedorRepository.Remover(id);
        }
    }
}

