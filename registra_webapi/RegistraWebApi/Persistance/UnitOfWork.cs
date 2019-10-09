using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly object context;

        public UnitOfWork(object context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
