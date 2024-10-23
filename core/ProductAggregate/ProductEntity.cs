using Ardalis.SharedKernel;

namespace core.ProductAggregate;

public class ProductEntity : EntityBase<Guid>,
    IAggregateRoot
{
    public string? SKU { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public static ProductEntity CreatProductEntity(string sku,string name,decimal price,int stock)
    {
        return new ProductEntity
        {
            SKU = sku,
            Name = name,
            Price = price,
            Stock = stock
        };
    }

    public static void UpdateProductEntity(ProductEntity product,string requestSku, string requestName, decimal requestPrice, int requestStock)
    {
        product.Name = requestName;
        product.SKU = requestSku;
        product.Price = requestPrice;
        product.Stock = requestStock;
    }
}