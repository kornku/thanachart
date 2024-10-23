using Ardalis.Result;
using Ardalis.SharedKernel;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Product;

public record GetByIdProductCommand(Guid ProductId) : ICommand<Result<ProductResponse>>;

public class GetByIdProductHandler
    (IRepository<ProductEntity> repository) : ICommandHandler<GetByIdProductCommand, Result<ProductResponse>>
{
    public async Task<Result<ProductResponse>> Handle(GetByIdProductCommand request,
        CancellationToken cancellationToken)
    {
        var selected = await repository.FirstOrDefaultAsync(new GetByProductByIdSpec(request.ProductId),
            cancellationToken);

        if (selected == null)
        {
            return Result.Conflict();
        }

        var result = new ProductResponse
        {
            Id = selected.Id,
            Sku = selected.SKU,
            Price = selected.Price
        };

        return Result.Success(result);
    }
}