using LRC.Business.Entidades;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Interfaces.Servicos
{
    public interface ILogAlteracaoService : IService<LogAlteracao>
    {
        Task CompararAlteracoes<T>(T objetoAntigo, T objetoNovo, Guid usuarioId, String chave);
        Task CompararAlteracoesComFiltros<T>(T objetoAntigo, T objetoNovo, Guid usuarioId, String chave, ObjectsComparer.Comparer<T> comparer);
        Task RegistrarLogModificacao(IEnumerable<Difference> diferencas, Guid usuarioId, String chave);
        Task RegistrarLogDiretamente(String historico, Guid usuarioId, String chave);
    }
}
