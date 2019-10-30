using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RegistraWebApi.Models
{
    public class User : IdentityUser<int>
    {
        public string LoginEmail { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}