using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.CartAggregate.Specifications;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Cart;


public record IncreaseCartCommand(Guid id,List<CartProductResponse> list) : ICommand<Result<bool>>;

public class DeceaseCartHandler
( IRepository<CartEntity> cartRepository,IRepository<CartProductEntity> cartProductRepository,
    IRepository<ProductEntity> productRepository) : ICommandHandler<IncreaseCartCommand,
    Result<bool>>
{
    public async Task<Result<bool>> Handle(IncreaseCartCommand request, CancellationToken cancellationToken)
    {
        var selected = await cartRepository.FirstOrDefaultAsync(new GetCartByIdSpec(request.id),
            cancellationToken);

        if (selected == null)
        {
            return Result.Error("id not exist");
        }

        foreach (var product in request.list)
        {
            var selectedCartProduct = await cartProductRepository.FirstOrDefaultAsync(new GetCartProductByIdSpec(request.id),
                cancellationToken);

            if (selectedCartProduct == null)
            {
                return Result.Error("id not exists");
            }

            var selectedProduct =
                await productRepository.FirstOrDefaultAsync(new GetByProductByIdSpec(selectedCartProduct.ProductId));

            if (selectedProduct == null)
            {
                return Result.Error("id not exists");
            }
            
            if (selectedCartProduct.Total > selectedProduct.Stock)
            {
                continue;
            }

            selectedCartProduct.Total = product.Total;
        }

        selected.Products = request.list.Select(x => new CartProductEntity
        {
            CartId = selected.Id,
            ProductId = x.ProductId,
            Total = x.Total
        }).ToList();


        await cartRepository.UpdateAsync(selected, cancellationToken);

        return Result.Success(true);
    }
}