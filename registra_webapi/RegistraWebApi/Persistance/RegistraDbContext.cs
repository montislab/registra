using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    public class RegistraDbContext : DbContext
    {
        public RegistraDbContext(DbContextOptions<RegistraDbContext> options)
            : base(options)
        {

        }
    }
}
