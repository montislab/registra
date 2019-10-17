namespace RegistraWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LoginEmail { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}