using System.ComponentModel.DataAnnotations;

namespace RegistraWebApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string LoginEmail { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Has�o musi zawiera� co najminej 8 znak�w.")]
        public string Password { get; set; }
    }
}