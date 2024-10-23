using Ardalis.Specification;
using core.ProductAggregate;

namespace core.CartAggregate.Specifications;

public class GetCartByIdSpec:Specification<CartEntity>
{
    public GetCartByIdSpec(Guid id)
    {
        Query
            .Include(x => x.Products)
            .Where(t => t.Id == id);
    }
}