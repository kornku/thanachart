using Ardalis.Result;
using Ardalis.SharedKernel;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Product;

public record GetProductCommand(string Keyword, int Page, int PageSize) : ICommand<Result<PagedList<ProductResponse>>>;

public class GetProductHandler
    (IRepository<ProductEntity> repository) : ICommandHandler<GetProductCommand, Result<PagedList<ProductResponse>>>
{
    public async Task<Result<PagedList<ProductResponse>>> Handle(GetProductCommand request,
        CancellationToken cancellationToken)
    {
        var spec = new GetByProductListSpec(request.Keyword, request.Page, request.PageSize);
        var list = await repository.ListAsync(spec,
            cancellationToken);

        var dtoList = list.Select(x => new ProductResponse
        {
            Id = x.Id,
            Sku = x.SKU,
            Price = x.Price
        }).ToList();

        var total = await repository.CountAsync(spec, cancellationToken);

        var paging = new PagedList<ProductResponse>(dtoList, total, request.Page, request.PageSize);

        return Result.Success(paging);
    }
}