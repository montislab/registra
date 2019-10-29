using Microsoft.EntityFrameworkCore;
using RegistraWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Persistance
{
    public class RegistraDbContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        
        public RegistraDbContext(DbContextOptions<RegistraDbContext> options)
            : base(options)
        {

        }
    }
}
