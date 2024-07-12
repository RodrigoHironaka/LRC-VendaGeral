using LRC.Business.Entidades;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface IContaPagarRepository : IRepository<ContaPagar>
    {
        Task<ContaPagar> ObterPorIdComFornecedor(Guid id);
        Task<IEnumerable<ContaPagar>> ObterTodosComFornecedor();
    }
}
