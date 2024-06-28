using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;

namespace LRC.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext db) : base(db) { }

        //public Task<Cliente> ObterClienteEndereco(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
