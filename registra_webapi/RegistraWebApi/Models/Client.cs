using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
