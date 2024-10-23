using Ardalis.SharedKernel;

namespace core.ProductAggregate;

public class StockEntity : EntityBase<Guid>,
    IAggregateRoot
{
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = new();
    public int Total { get; set; }
}