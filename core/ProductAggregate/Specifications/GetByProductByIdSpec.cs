using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace core.ProductAggregate.Specifications;

public class GetByProductByIdSpec:Specification<ProductEntity>
{
    public GetByProductByIdSpec(Guid id)
    {
        Query
            .Where(t => t.Id == id);
    }
}