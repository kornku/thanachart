namespace usecases.Cart;

public class CartResponse
{
    public Guid Id { get; set; }
    public List<CartProductResponse> List { get; set; } = new List<CartProductResponse>();
}