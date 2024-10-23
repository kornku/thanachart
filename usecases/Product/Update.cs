using Ardalis.Result;
using Ardalis.SharedKernel;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Product;


public record UpdateProductCommand(Guid ProductId,string Name,string Sku,decimal Price,int Stock) : ICommand<Result<ProductResponse>>;

public class UpdateProductProductHandler
    (IRepository<ProductEntity> repository) : ICommandHandler<UpdateProductCommand, Result<ProductResponse>>
{
    public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var selected = await repository.FirstOrDefaultAsync(new GetByProductByIdSpec(request.ProductId),
            cancellationToken);

        if (selected == null)
        {
            return Result.Conflict();
        }

        ProductEntity.UpdateProductEntity(selected,request.Sku,request.Name,request.Price,request.Stock);

        selected.Stock = request.Stock;

        await repository.UpdateAsync(selected, cancellationToken);
        var result = new ProductResponse
        {
            Id = selected.Id,
            Sku = selected.SKU,
            Price = selected.Price
        };

        return Result.Success(result);
    }
}