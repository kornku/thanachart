namespace usecases.Cart;

public class CartProductResponse
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Total { get; set; }
}