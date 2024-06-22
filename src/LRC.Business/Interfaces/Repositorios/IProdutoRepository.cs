using LRC.Business.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Interfaces.Repositorios
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterPorIdComSubGrupo(Guid id);
        Task<IEnumerable<Produto>> ObterTodosComSubGrupo();
    }
}
