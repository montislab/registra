using RegistraWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RegistraWebApi.Persistance.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public RegistraDbContext RegistraDbContext
        {
            get => DbContext as RegistraDbContext;
        }
        
        public ClientRepository(RegistraDbContext registraDbContext)
            :base(registraDbContext)
        {
        }

        public IEnumerable<Client> GetBestClients()
        {
            return RegistraDbContext.Clients.OrderByDescending(p => p.FirstName).Take(2).ToList();
        }

    }
}
