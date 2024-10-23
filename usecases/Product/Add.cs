using Ardalis.Result;
using Ardalis.SharedKernel;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Product;


public record CreateProductCommand(string Name,string Sku,decimal Price,int Stock) : ICommand<Result<Guid>>;

public class CreateProductHandler
    (IRepository<ProductEntity> repository) : ICommandHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var selected = await repository.FirstOrDefaultAsync(new GetByProductSpec(request.Sku.ToUpperInvariant()),
            cancellationToken);

        if (selected != null)
        {
            return Result.Conflict();
        }

        var entity = ProductEntity.CreatProductEntity(request.Sku,request.Name,request.Price,request.Stock);
        
        var createdItem = await repository.AddAsync(
            entity, cancellationToken);
        return Result.Success(createdItem.Id);
    }
}