using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LRC.Data.Repository
{
    public class ContaPagarRepository : Repository<ContaPagar>, IContaPagarRepository
    {
        public ContaPagarRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<ContaPagar>> ObterTodosComFornecedor()
        {
            return await Db.ContasPagar.AsNoTracking().Include(f => f.Fornecedor)
                .OrderBy(p => p.Descricao).ToListAsync();
        }

        public async Task<ContaPagar> ObterPorIdComFornecedor(Guid id)
        {
            var contaPagar = await Db.ContasPagar.AsNoTracking().Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (contaPagar == null)
                return new ContaPagar();
            return contaPagar;
        }
    }
}
