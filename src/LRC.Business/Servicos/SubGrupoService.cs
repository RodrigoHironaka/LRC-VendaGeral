using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using System.Linq.Expressions;

namespace LRC.Business.Servicos
{
    public class SubGrupoService : BaseService, ISubGrupoService
    {
        private readonly ISubGrupoRepository _subGrupoRepository;

        public SubGrupoService(ISubGrupoRepository subGrupoRepository, 
            INotificador notificador) : base(notificador)
        {
            _subGrupoRepository = subGrupoRepository;
        }

        public async Task<Subgrupo> ObterPorId(Guid id)
        {
            return await _subGrupoRepository.ObterPorId(id);
        }

        public async Task<List<Subgrupo>> ObterTodos()
        {
            return await _subGrupoRepository.ObterTodos();
        }

        public async Task Adicionar(Subgrupo entity)
        {
            if (!ExecutarValidacao(new SubGrupoValidation(), entity)) return;
            await _subGrupoRepository.Adicionar(entity);
        }

        public async Task Atualizar(Subgrupo entity)
        {
            if (!ExecutarValidacao(new SubGrupoValidation(), entity)) return;
            await _subGrupoRepository.Atualizar(entity);
        }

        public async Task Remover(Guid id)
        {
            await _subGrupoRepository.Remover(id);
        }

        public async Task<IEnumerable<Subgrupo>> Buscar(Expression<Func<Subgrupo, bool>> predicate)
        {
            return await _subGrupoRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _subGrupoRepository?.Dispose();
        }
    }
}
