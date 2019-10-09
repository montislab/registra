using RegistraWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistraWebApi.Persistance.Repository;

namespace RegistraWebApi.Persistance.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(RegistraDbContext registraDbContext)
            :base(registraDbContext)
        {
        }

        public IEnumerable<Client> GetBestClients()
        {
            return RegistraDbContext.Clients.OrderByDescending(p => p.FirstName).Take(2).ToList();
        }

        public RegistraDbContext RegistraDbContext
        {
            get => DbContext as RegistraDbContext;
        }
    }
}
