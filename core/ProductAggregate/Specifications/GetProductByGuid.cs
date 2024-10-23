using Ardalis.Specification;

namespace core.ProductAggregate.Specifications;

public class GetProductByGuid:Specification<ProductEntity>
{
    public GetProductByGuid(List<Guid> skuList)
    {
        Query.Where(t => skuList.Contains(t.Id));
    }
}

