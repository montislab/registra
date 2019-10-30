using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegistraWebApi.Models;

namespace RegistraWebApi.Persistance.Repository
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public RegistraDbContext RegistraDbContext
        {
            get => DbContext as RegistraDbContext;
        }

        public AuthRepository(RegistraDbContext registraDbContext) 
            : base(registraDbContext)
        {
        }


        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassowrdHash(password, out passwordHash, out passwordSalt);

            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            await RegistraDbContext.Users.AddAsync(user);
            //await RegistraDbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePassowrdHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await RegistraDbContext.Users.FirstOrDefaultAsync(x => x.LoginEmail == username);

            if(user == null)
                return null;
            
            //if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }                
            }
            return true;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await RegistraDbContext.Users.AnyAsync(x => x.LoginEmail == username))
                return true;
            
            return false;
        }
    }
}