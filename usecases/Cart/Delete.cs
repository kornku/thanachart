using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.CartAggregate.Specifications;

namespace usecases.Cart;


public record DeleteCartCommand(Guid id) : ICommand<Result<bool>>;

public class DeleteCartHandler
    ( IRepository<CartEntity> cartRepository) : ICommandHandler<DeleteCartCommand,
        Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var selected = await cartRepository.FirstOrDefaultAsync(new GetCartByIdSpec(request.id),
            cancellationToken);

        if (selected == null)
        {
            return Result.Error("id not exist");
        }

        await cartRepository.DeleteAsync(selected, cancellationToken);

        return Result.Success(true);
    }
}