using RegistraWebApi.Models;
using System.Collections.Generic;
using RegistraWebApi.Persistance.Repository;

namespace RegistraWebApi.Persistance.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> GetBestClients();
    }
}
