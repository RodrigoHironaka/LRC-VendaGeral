using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Data.Repository
{
    public class LogAlteracaoRepository : Repository<LogAlteracao>, ILogAlteracaoRepository
    {
        public LogAlteracaoRepository(MeuDbContext db) : base(db) { }
    }
}
