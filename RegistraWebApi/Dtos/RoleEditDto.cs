using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Dtos
{
    public class RoleEditDto
    {
        public string UserName { get; set; }
        public string[] RoleNames { get; set; }
    }
}
