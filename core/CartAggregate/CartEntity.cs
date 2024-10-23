using Ardalis.SharedKernel;

namespace core.CartAggregate;

public class CartEntity: EntityBase<Guid>,
    IAggregateRoot
{
    public List<CartProductEntity> Products { get; set; } = new();
}