using LRC.Business.Entidades;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface IContaReceberRepository : IRepository<ContaReceber>
    {
        Task<ContaReceber> ObterPorIdComCliente(Guid id);
        Task<IEnumerable<ContaReceber>> ObterTodosComCliente();
    }
}
