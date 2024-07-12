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
    public class FormaPagamentoService : BaseService, IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;

        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepository, INotificador notificador) : base(notificador)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<FormaPagamento> ObterPorId(Guid id)
        {
            return await _formaPagamentoRepository.ObterPorId(id);
        }

        public async Task<List<FormaPagamento>> ObterTodos()
        {
            return await _formaPagamentoRepository.ObterTodos();
        }

        public async Task Adicionar(FormaPagamento entity)
        {
            if (!ExecutarValidacao(new FormaPagamentoValidation(), entity)) return;
            await _formaPagamentoRepository.Adicionar(entity);
        }

        public async Task Atualizar(FormaPagamento entity)
        {
            if (!ExecutarValidacao(new FormaPagamentoValidation(), entity)) return;
            await _formaPagamentoRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _formaPagamentoRepository.Remover(id);
        }

        public async Task<IEnumerable<FormaPagamento>> Buscar(Expression<Func<FormaPagamento, bool>> predicate)
        {
            return await _formaPagamentoRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _formaPagamentoRepository?.Dispose();
        }
    }
}
