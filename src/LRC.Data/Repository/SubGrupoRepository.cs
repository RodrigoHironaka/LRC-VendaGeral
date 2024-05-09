using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Data.Repository
{
    public class SubGrupoRepository : Repository<Subgrupo>, ISubGrupoRepository
    {
        public SubGrupoRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<Subgrupo>> ObterTodosComGrupo()
        {
            return await Db.SubGrupos.AsNoTracking().Include(f => f.Grupo)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Subgrupo> ObterPorIdComGrupo(Guid id)
        {
            var subGrupo = await Db.SubGrupos.AsNoTracking().Include(f => f.Grupo)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (subGrupo == null)
                return new Subgrupo();
            return subGrupo;
        }
    }
}
