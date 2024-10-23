using System.Globalization;
using Ardalis.Result;
using Ardalis.SharedKernel;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Product;

public record DeleteProductCommand(Guid ProductId) : ICommand<Result<bool>>;

public class DeleteProductHandler
    (IRepository<ProductEntity> repository) : ICommandHandler<DeleteProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var selected = await repository.FirstOrDefaultAsync(new GetByProductByIdSpec(request.ProductId),
            cancellationToken);

        if (selected == null)
        {
            return Result.Conflict();
        }


        try
        {
            await repository.DeleteAsync(
                selected, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message);
        }
    }
}