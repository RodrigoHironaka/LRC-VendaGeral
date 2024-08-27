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
    public class FluxoCaixaService : BaseService, IFluxoCaixaService
    {
        private readonly IFluxoCaixaRepository _fluxoCaixaRepository;

        public FluxoCaixaService(IFluxoCaixaRepository fluxoCaixaRepository, INotificador notificador) : base(notificador)
        {
            _fluxoCaixaRepository = fluxoCaixaRepository;
        }

        public async Task<FluxoCaixa> ObterPorId(Guid id)
        {
            return await _fluxoCaixaRepository.ObterPorId(id);
        }

        public async Task<List<FluxoCaixa>> ObterTodos()
        {
            return await _fluxoCaixaRepository.ObterTodos();
        }

        public async Task Adicionar(FluxoCaixa entity)
        {
            if (!ExecutarValidacao(new FluxoCaixaValidation(), entity)) return;
            await _fluxoCaixaRepository.Adicionar(entity);
        }

        public async Task Atualizar(FluxoCaixa entity)
        {
            if (!ExecutarValidacao(new FluxoCaixaValidation(), entity)) return;
            await _fluxoCaixaRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _fluxoCaixaRepository.Remover(id);
        }

        public async Task<IEnumerable<FluxoCaixa>> Buscar(Expression<Func<FluxoCaixa, bool>> predicate)
        {
            return await _fluxoCaixaRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _fluxoCaixaRepository?.Dispose();
        }
    }
}

