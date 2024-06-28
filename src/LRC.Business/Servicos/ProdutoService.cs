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
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        public async Task<List<Produto>> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        public async Task Adicionar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity)) return;
            await _produtoRepository.Adicionar(entity);
        }

        public async Task Atualizar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity)) return;
            await _produtoRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public async Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> predicate)
        {
            return await _produtoRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
