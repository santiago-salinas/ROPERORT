namespace Rest_Api.DTOs
{
    public class CartDTO
    {
        public List<CartLineDTO> Products { get; set; }

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