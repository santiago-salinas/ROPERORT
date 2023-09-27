namespace Rest_Api.DTOs
{
    public class CredentialsDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }

        public CredentialsDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
