using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.CartAggregate.Specifications;

namespace usecases.Cart;


public record GetCartByIdCommand(Guid id) : ICommand<Result<CartResponse>>;

public class GetByIdHandler
    ( IRepository<CartEntity> cartRepository) : ICommandHandler<GetCartByIdCommand,
        Result<CartResponse>>
{
    public async Task<Result<CartResponse>> Handle(GetCartByIdCommand request, CancellationToken cancellationToken)
    {
        var selected = await cartRepository.FirstOrDefaultAsync(new GetCartByIdSpec(request.id),
            cancellationToken);

        if (selected == null)
        {
            return Result.Error("id not exist");
        }

        var result = new CartResponse
        {
            Id = selected.Id,
            List = selected.Products.Select(xx => new CartProductResponse
            {
                ProductId = xx.ProductId,
                Total = xx.Total
            }).ToList()
        };

        return Result.Success(result);
    }
}