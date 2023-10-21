using Services.Models.PaymentMethods;

namespace Rest_Api.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public TokenDTO(string token) 
        {
            Token = token;
        }
    }
}