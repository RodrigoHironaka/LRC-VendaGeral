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
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository clienteRepository,
        IEnderecoRepository enderecoRepository,
            INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Cliente entity)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;

            //Validar se nao existe o cliente com o mesmo documento
            if (_clienteRepository.Buscar(f => f.Documento == entity.Documento).Result.Any())
            {
                Notificar("Já existe um cliente com este documento informado.");
                return;
            }
            await _clienteRepository.Adicionar(entity);
        }

        public async Task Atualizar(Cliente entity)
        {
            if (!ExecutarValidacao(new ClienteValidation(), entity)) return;
            if (_clienteRepository.Buscar(f => f.Documento == entity.Documento && f.Id != entity.Id).Result.Any())
            {
                Notificar("Já existe um cliente com este documento informado.");
                return;
            }
            await _clienteRepository.Atualizar(entity);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            return await _clienteRepository.Buscar(predicate);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

        public async Task<Cliente> ObterClienteEndereco(Guid id)
        {
            return await _clienteRepository.ObterClienteEndereco(id);
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
            //if (_clienteRepository.ObterClienteEndereco(id).Result.Produtos.Any())
            //{
            //    Notificar("O Cliente possui produtos cadastrados!");
            //    return;
            //}

            var endereco = await _enderecoRepository.ObterEnderecoPorCliente(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _clienteRepository.Remover(id);
        }
    }
}
