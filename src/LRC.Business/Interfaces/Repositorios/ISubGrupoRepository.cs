using LRC.Business.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface ISubGrupoRepository : IRepository<Subgrupo>
    {
        Task<Subgrupo> ObterPorIdComGrupo(Guid id);
        Task<IEnumerable<Subgrupo>> ObterTodosComGrupo();
    }
}
