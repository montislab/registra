using RegistraWebApi.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        int SaveChanges();
    }
}
