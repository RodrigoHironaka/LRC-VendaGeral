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
    public class CaixaService : BaseService, ICaixaService
    {
        private readonly ICaixaRepository _caixaRepository;

        public CaixaService(ICaixaRepository caixaRepository, INotificador notificador) : base(notificador)
        {
            _caixaRepository = caixaRepository;
        }

        public async Task<Caixa> ObterPorId(Guid id)
        {
            return await _caixaRepository.ObterPorId(id);
        }

        public async Task<List<Caixa>> ObterTodos()
        {
            return await _caixaRepository.ObterTodos();
        }

        public async Task Adicionar(Caixa entity)
        {
            if (!ExecutarValidacao(new CaixaValidation(), entity)) return;
            await _caixaRepository.Adicionar(entity);
        }

        public async Task Atualizar(Caixa entity)
        {
            if (!ExecutarValidacao(new CaixaValidation(), entity)) return;
            await _caixaRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _caixaRepository.Remover(id);
        }

        public async Task<IEnumerable<Caixa>> Buscar(Expression<Func<Caixa, bool>> predicate)
        {
            return await _caixaRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _caixaRepository?.Dispose();
        }
    }
}
