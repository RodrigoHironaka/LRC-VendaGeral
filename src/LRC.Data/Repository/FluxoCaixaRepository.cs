using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Data.Repository
{
    public class FluxoCaixaRepository : Repository<FluxoCaixa>, IFluxoCaixaRepository
    {
        public FluxoCaixaRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<FluxoCaixa>> ObterTodosComEntidades(Guid IdCaixa)
        {
            return await Db.FluxosCaixa.Where(x => x.CaixaId == IdCaixa).AsNoTracking().Include(f => f.FormaPagamento).Include(f => f.Caixa)
                .OrderBy(p => p.CaixaId).ToListAsync();
        }

        public async Task<FluxoCaixa> ObterPorIdComEntidade(Guid id, Guid IdCaixa)
        {
            var FluxoCaixa = await Db.FluxosCaixa.Where(x => x.CaixaId == IdCaixa).AsNoTracking().Include(f => f.FormaPagamento).Include(f => f.Caixa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (FluxoCaixa == null)
                return new FluxoCaixa();
            return FluxoCaixa;
        }
    }
}
