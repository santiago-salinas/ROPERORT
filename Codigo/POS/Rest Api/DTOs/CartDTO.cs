using Services.Models.PaymentMethods;

namespace Rest_Api.DTOs
{
    public class CartDTO
    {
        public List<CartLineDTO> Products { get; set; }

        public string? PaymentMethod { get; set; }
        public string? PaymentId { get; set; }
        public string? Bank { get; set; }
        public string? Company { get; set; }

        public CartDTO()
        {
            Products = new List<CartLineDTO>();
        }
    }
    public class CartLineDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}