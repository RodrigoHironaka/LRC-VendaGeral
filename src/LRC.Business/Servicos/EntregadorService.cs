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
    public class EntregadorService : BaseService, IEntregadorService
    {
        private readonly IEntregadorRepository _entregadorRepository;

        public EntregadorService(IEntregadorRepository entregadorRepository,
            INotificador notificador) : base(notificador)
        {
            _entregadorRepository = entregadorRepository;
        }

        public async Task Adicionar(Entregador entity)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new EntregadorValidation(), entity)) return;
            await _entregadorRepository.Adicionar(entity);
        }

        public async Task Atualizar(Entregador entity)
        {
            if (!ExecutarValidacao(new EntregadorValidation(), entity)) return;
            await _entregadorRepository.Atualizar(entity);
        }

        public async Task<IEnumerable<Entregador>> Buscar(Expression<Func<Entregador, bool>> predicate)
        {
            return await _entregadorRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _entregadorRepository?.Dispose();
        }

        public async Task<Entregador> ObterPorId(Guid id)
        {
            return await _entregadorRepository.ObterPorId(id);
        }

        public async Task<List<Entregador>> ObterTodos()
        {
            return await _entregadorRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _entregadorRepository.Remover(id);
        }
    }
}

