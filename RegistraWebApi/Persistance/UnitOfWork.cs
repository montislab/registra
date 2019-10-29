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
        public IAuthRepository AuthRepository { get; private set; }

        public UnitOfWork(RegistraDbContext registraDbContext,
                          IClientRepository clientRepository,
                          IAuthRepository authRepository)
        {
            this.registraDbContext = registraDbContext;
            Clients = clientRepository;
            AuthRepository = authRepository;
        }

        public int SaveChanges()
        {
            return registraDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await registraDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            registraDbContext.Dispose();
        }
    }
}
