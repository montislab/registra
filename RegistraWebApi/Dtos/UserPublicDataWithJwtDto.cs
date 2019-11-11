namespace RegistraWebApi.Dtos
{
    public class UserPublicDataWithJwtDto
    {
        public string token { get; set; }
        public UserDto user { get; set; }
    }
}
