using LRC.Business.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Interfaces.Servicos
{
    public interface IEnderecoService : IService<Endereco>
    {
        Task<Endereco> ObterEnderecoPorCliente(Guid clienteId);
    }
}
