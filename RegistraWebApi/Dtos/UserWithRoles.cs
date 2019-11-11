using System.Collections.Generic;

namespace RegistraWebApi.Dtos
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
