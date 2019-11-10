using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistraWebApi.Dtos
{
    public class UserPublicDataWithJwtDto
    {
        public string token { get; set; }
        public UserDto user { get; set; }
    }
}
