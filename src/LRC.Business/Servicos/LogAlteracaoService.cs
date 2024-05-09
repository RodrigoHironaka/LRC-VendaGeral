using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;
using System.Linq.Expressions;
using ObjectsComparer;

namespace LRC.Business.Servicos
{
    public class LogAlteracaoService : BaseService, ILogAlteracaoService
    {
        private readonly ILogAlteracaoRepository _logAlteracaoRepository;

        public LogAlteracaoService(ILogAlteracaoRepository logAlteracaoRepository, INotificador notificador) : base(notificador)
        {
            _logAlteracaoRepository = logAlteracaoRepository;
        }

        public async Task Adicionar(LogAlteracao entity)
        {
            if (!ExecutarValidacao(new LogAlteracaoValidation(), entity)) return;
            await _logAlteracaoRepository.Adicionar(entity);
        }

        public async Task Atualizar(LogAlteracao entity)
        {
            if (!ExecutarValidacao(new LogAlteracaoValidation(), entity)) return;
            await _logAlteracaoRepository.Atualizar(entity);
        }

        public async Task<IEnumerable<LogAlteracao>> Buscar(Expression<Func<LogAlteracao, bool>> predicate)
        {
            return await _logAlteracaoRepository.Buscar(predicate);
        }

        
        public void Dispose()
        {
            _logAlteracaoRepository?.Dispose();
        }

        public async Task<LogAlteracao> ObterPorId(Guid id)
        {
            return await _logAlteracaoRepository.ObterPorId(id);
        }

        public async Task<List<LogAlteracao>> ObterTodos()
        {
            return await _logAlteracaoRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _logAlteracaoRepository.Remover(id);
        }

        public async Task CompararAlteracoes<T>(T objetoAntigo, T objetoNovo, Guid usuarioId, string chave)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            //comparer.AddComparerOverride<Usuario>(DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride<Guid>(DoNotCompareValueComparer.Instance, member => member.Name.Contains("Id"));
            comparer.IgnoreMember("DataCadastro");
            comparer.IgnoreMember("DataAlteracao");
            var igual = comparer.Compare(objetoAntigo, objetoNovo, out IEnumerable<Difference> diferencas);
            if (!igual)
            {
                await RegistrarLogModificacao(diferencas, usuarioId, chave);
            }
        }

        public async Task CompararAlteracoesComFiltros<T>(T objetoAntigo, T objetoNovo, Guid usuarioId, string chave, ObjectsComparer.Comparer<T> comparer)
        {
            comparer.AddComparerOverride<Guid>(DoNotCompareValueComparer.Instance, member => member.Name.Contains("Id"));
            comparer.IgnoreMember("DataCadastro");
            comparer.IgnoreMember("DataAlteracao");
            var igual = comparer.Compare(objetoAntigo, objetoNovo, out IEnumerable<Difference> diferencas);
            if (!igual)
            {
                await RegistrarLogModificacao(diferencas, usuarioId, chave);
            }
        }

        public async Task RegistrarLogModificacao(IEnumerable<Difference> diferencas, Guid usuarioId, string chave)
        {
            var historico = new StringBuilder();
            foreach (var item in diferencas)
            {
                if (item.Value1 != item.Value2 &&
                    item.Value2 != DateTime.MinValue.ToString() &&
                    !item.MemberPath.EndsWith(".DataGeracao.Value") &&
                    !item.MemberPath.EndsWith(".DataAlteracao.Value"))
                {
                    if (item.MemberPath.Contains("."))
                    {
                        if (item.MemberPath.EndsWith(".Nome") || item.MemberPath.EndsWith(".Value"))
                            historico.AppendLine(String.Format("Campo: {0} | Antes: {1} | Depois: {2}", item.MemberPath, item.Value1, item.Value2));
                        else
                        {
                            if(!String.IsNullOrEmpty(item.Value1) && !String.IsNullOrEmpty(item.Value2))
                            {
                                if (item.Value1 != item.Value2)
                                    historico.AppendLine(String.Format("Campo: {0} | Antes: {1} | Depois: {2}", item.MemberPath, item.Value1, item.Value2));
                            }
                            
                        }
                    }
                    else 
                        historico.AppendLine(String.Format("Campo: {0} | Antes: {1} | Depois: {2}", item.MemberPath, item.Value1, item.Value2));
                }
            }

            if (historico.Length > 0)
            {
                var log = new LogAlteracao
                {
                    DataCadastro = DateTime.Now,
                    UsuarioCadastroId = usuarioId,
                    Historico = historico.ToString(),
                    Chave = chave,
                };
                await Adicionar(log);
            }
        }
        public async Task RegistrarLogDiretamente(string historico, Guid usuarioId, string chave)
        {
            if (historico.Length > 0)
            {
                var log = new LogAlteracao
                {
                    DataCadastro = DateTime.Now,
                    UsuarioCadastroId = usuarioId,
                    Historico = historico.ToString(),
                    Chave = chave,
                };
                await Adicionar(log);

            }
        }

    }
}
