using Ardalis.Specification;

namespace core.ProductAggregate.Specifications;

public class GetByStockByIdSpec:Specification<StockEntity>
{
    public GetByStockByIdSpec(Guid id)
    {
        Query
            .Where(t => t.Id == id);
    }
}