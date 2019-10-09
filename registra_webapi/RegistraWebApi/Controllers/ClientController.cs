using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistraWebApi.Models;
using RegistraWebApi.Persistance;

namespace RegistraWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        public ClientController(RegistraDbContext registraDbContext)
        {
            
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {

        }
    }
}
