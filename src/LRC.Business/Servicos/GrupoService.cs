using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using System.Linq.Expressions;

namespace LRC.Business.Servicos
{
    public class GrupoService : BaseService, IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository, INotificador notificador) : base(notificador)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task Adicionar(Grupo entity)
        {
            if (!ExecutarValidacao(new GrupoValidation(), entity)) return;
            await _grupoRepository.Adicionar(entity);
        }

        public async Task Atualizar(Grupo entity)
        {
            if (!ExecutarValidacao(new GrupoValidation(), entity)) return;
            await _grupoRepository.Atualizar(entity);
        }

        public async Task<IEnumerable<Grupo>> Buscar(Expression<Func<Grupo, bool>> predicate)
        {
            return await _grupoRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _grupoRepository?.Dispose();
        }

        public async Task<Grupo> ObterPorId(Guid id)
        {
            return await _grupoRepository.ObterPorId(id);
        }

        public async Task<List<Grupo>> ObterTodos()
        {
            return await _grupoRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _grupoRepository.Remover(id);
        }
    }
}
