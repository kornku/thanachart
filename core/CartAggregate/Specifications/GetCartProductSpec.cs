using Ardalis.Specification;

namespace core.CartAggregate.Specifications;

public class GetCartProductByIdSpec:Specification<CartProductEntity>
{
    public GetCartProductByIdSpec(Guid id)
    {
        Query
            .Where(t => t.Id == id);
    }
}