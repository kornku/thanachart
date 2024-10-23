using Ardalis.SharedKernel;
using core.ProductAggregate;

namespace core.CartAggregate;

public class CartProductEntity: EntityBase<Guid>,
    IAggregateRoot
{
    public Guid CartId { get; set; }
    public CartEntity CartEntity { get; set; } = new();
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = new();
    public int Total { get; set; }
}