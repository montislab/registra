using System.Threading.Tasks;
using RegistraWebApi.Models;

namespace RegistraWebApi.Persistance.Repository
{
    public interface IAuthRepository : IRepository<User>
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}