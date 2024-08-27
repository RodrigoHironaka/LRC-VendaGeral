using LRC.Business.Entidades;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface ICaixaRepository : IRepository<Caixa>
    {
        Task<Caixa> ObterCaixaAberto(string idUsuario);
        Task<Int64> ObteNumeroUltimoCaixa(string idUsuario);
        Task<Caixa> ObterPorIdComFluxosDeCaixa(Guid Id);
    }
}
