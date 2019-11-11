using RegistraWebApi.Persistance.Repository;
using System;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
