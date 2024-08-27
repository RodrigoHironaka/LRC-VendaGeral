using LRC.Business.Entidades;
using LRC.Business.Entidades.Enums;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LRC.Data.Repository
{
    public class CaixaRepository : Repository<Caixa>, ICaixaRepository
    {
        public CaixaRepository(MeuDbContext db) : base(db) { }

        public async Task<Int64> ObteNumeroUltimoCaixa(string idUsuario)
        {
            var caixas = await Buscar(x => x.UsuarioCadastroId == Guid.Parse(idUsuario));
            var ultimoCaixa = caixas.OrderBy(x => x.Numero).LastOrDefault();
            if(ultimoCaixa != null)
                return ultimoCaixa.Numero;
            
            return 0;
        }

        public async Task<Caixa> ObterCaixaAberto(string idUsuario)
        {
            var caixa = await Db.Caixas.AsNoTracking().Include(x => x.FluxosCaixa).Where(x => x.Situacao == SituacaoCaixa.Aberto && x.UsuarioCadastroId == Guid.Parse(idUsuario)).FirstOrDefaultAsync();
            return caixa;
        }

        public async Task<Caixa> ObterPorIdComFluxosDeCaixa(Guid Id)
        {
            var caixa = await Db.Caixas.AsNoTracking().Include(f => f.FluxosCaixa).ThenInclude(fc => fc.FormaPagamento)
                .FirstOrDefaultAsync(p => p.Id == Id);

            return caixa;
        }
    }
}
