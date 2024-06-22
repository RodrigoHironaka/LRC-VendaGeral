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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<Produto>> ObterTodosComSubGrupo()
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Subgrupo)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Produto> ObterPorIdComSubGrupo(Guid id)
        {
            var subGrupo = await Db.Produtos.AsNoTracking().Include(f => f.Subgrupo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (subGrupo == null)
                return new Produto();
            return subGrupo;
        }
    }
}
