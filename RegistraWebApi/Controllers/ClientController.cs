using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUnitOfWork unitOfWork;

        public ClientController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            var result = unitOfWork.Clients.GetAll();
            return result;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public Client Get(int id)
        {
            var result = unitOfWork.Clients.Get(id);
            return result;
        }
    }
}
