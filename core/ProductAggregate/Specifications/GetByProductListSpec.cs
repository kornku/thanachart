using Ardalis.Specification;

namespace core.ProductAggregate.Specifications;

public class GetByProductListSpec:Specification<ProductEntity>
{
    public GetByProductListSpec(string keyword,int page,int pageSize)
    {
        Query
            .Where(t => string.IsNullOrEmpty(keyword) || t.SKU!.Contains(keyword) || t.Name!.Contains(keyword)
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}