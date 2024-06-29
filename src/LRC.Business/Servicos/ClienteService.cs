using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Servicos
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository,
            INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Adicionar(Cliente entity)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;

            ////Validar se nao existe o cliente com o mesmo documento
            //if (_clienteRepository.Buscar(f => f.Documento == entity.Documento).Result.Any())
            //{
            //    Notificar("Já existe um cliente com este documento informado.");
            //    return;
            //}
            await _clienteRepository.Adicionar(entity);
        }

        public async Task Atualizar(Cliente entity)
        {
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;
            //if (_clienteRepository.Buscar(f => f.Documento == entity.Documento && f.Id != entity.Id).Result.Any())
            //{
            //    Notificar("Já existe um cliente com este documento informado.");
            //    return;
            //}
            await _clienteRepository.Atualizar(entity);
        }

        

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            return await _clienteRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }

        public async Task<List<Cliente>> ObterTodos()
        {
            return await _clienteRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _clienteRepository.Remover(id);
        }
    }
}
