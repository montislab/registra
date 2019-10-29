using System.ComponentModel.DataAnnotations;

namespace RegistraWebApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string LoginEmail { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Has³o musi zawieraæ co najminej 8 znaków.")]
        public string Password { get; set; }
    }
}