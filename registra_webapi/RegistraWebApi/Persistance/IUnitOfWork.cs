using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
