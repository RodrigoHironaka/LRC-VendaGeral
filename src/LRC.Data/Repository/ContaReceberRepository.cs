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
    public class ContaReceberRepository : Repository<ContaReceber>, IContaReceberRepository
    {
        public ContaReceberRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<ContaReceber>> ObterTodosComCliente()
        {
            return await Db.ContasReceber.AsNoTracking().Include(f => f.Cliente)
                .OrderBy(p => p.Descricao).ToListAsync();
        }

        public async Task<ContaReceber> ObterPorIdComCliente(Guid id)
        {
            var contaReceber = await Db.ContasReceber.AsNoTracking().Include(f => f.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (contaReceber == null)
                return new ContaReceber();
            return contaReceber;
        }
    }
}
