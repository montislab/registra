using RegistraWebApi.Models;
using System.Collections.Generic;

namespace RegistraWebApi.Persistance.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> GetBestClients();
    }
}
