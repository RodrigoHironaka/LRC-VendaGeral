using LRC.Business.Entidades;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface IFluxoCaixaRepository : IRepository<FluxoCaixa>
    {
        Task<FluxoCaixa> ObterPorIdComEntidade(Guid id, Guid idCaixa);
        Task<IEnumerable<FluxoCaixa>> ObterTodosComEntidades(Guid idCaixa);
    }
}
