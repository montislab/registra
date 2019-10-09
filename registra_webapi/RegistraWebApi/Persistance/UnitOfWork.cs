using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistraWebApi.Persistance.Repository;

namespace RegistraWebApi.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistraDbContext registraDbContext;
        public IClientRepository Clients { get; private set; }

        public UnitOfWork(RegistraDbContext registraDbContext,
                          IClientRepository ClientRepository)
        {
            this.registraDbContext = registraDbContext;
            Clients = ClientRepository;
        }

        public int SaveChanges()
        {
            return registraDbContext.SaveChanges();
        }
        public void Dispose()
        {
            registraDbContext.Dispose();
        }
    }
}
