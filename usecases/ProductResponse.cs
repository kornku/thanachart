namespace usecases.Product;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string? Sku { get; set; }
    public decimal Price { get; set; }
}