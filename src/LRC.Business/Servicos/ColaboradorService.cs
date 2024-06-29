using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using System.Linq.Expressions;

namespace LRC.Business.Servicos
{
    public class ColaboradorService : BaseService, IColaboradorService
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorService(IColaboradorRepository colaboradorRepository,
            INotificador notificador) : base(notificador)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task Adicionar(Colaborador entity)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new ColaboradorValidation(), entity)) return;
            await _colaboradorRepository.Adicionar(entity);
        }

        public async Task Atualizar(Colaborador entity)
        {
            if (!ExecutarValidacao(new ColaboradorValidation(), entity)) return;
            await _colaboradorRepository.Atualizar(entity);
        }

        public async Task<IEnumerable<Colaborador>> Buscar(Expression<Func<Colaborador, bool>> predicate)
        {
            return await _colaboradorRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _colaboradorRepository?.Dispose();
        }

        public async Task<Colaborador> ObterPorId(Guid id)
        {
            return await _colaboradorRepository.ObterPorId(id);
        }

        public async Task<List<Colaborador>> ObterTodos()
        {
            return await _colaboradorRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _colaboradorRepository.Remover(id);
        }
    }
}
