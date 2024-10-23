using Ardalis.Specification;

namespace core.ProductAggregate.Specifications;

public class GetByProductSpec:Specification<ProductEntity>
{
    public GetByProductSpec(string sku)
    {
        Query.Where(t => t.SKU == sku);
    }
}