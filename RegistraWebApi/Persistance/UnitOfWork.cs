using System.Threading.Tasks;
using RegistraWebApi.Persistance.Repository;

namespace RegistraWebApi.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistraDbContext registraDbContext;
        public IClientRepository Clients { get; private set; }

        public UnitOfWork(RegistraDbContext registraDbContext,
                          IClientRepository clientRepository)
        {
            this.registraDbContext = registraDbContext;
            Clients = clientRepository;
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
