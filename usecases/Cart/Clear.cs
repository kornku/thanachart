using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.CartAggregate.Specifications;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Cart;


public record ClearCartCommand(Guid id) : ICommand<Result<bool>>;

public class ClearCartHandler
    ( IRepository<CartEntity> cartRepository) : ICommandHandler<ClearCartCommand,
        Result<bool>>
{
    public async Task<Result<bool>> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var selected = await cartRepository.FirstOrDefaultAsync(new GetCartByIdSpec(request.id),
            cancellationToken);

        if (selected == null)
        {
            return Result.Error("id not exist");
        }

        selected.Products = new List<CartProductEntity>();

        await cartRepository.UpdateAsync(selected, cancellationToken);

        return Result.Success(true);
    }
}