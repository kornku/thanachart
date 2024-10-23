using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;
using usecases.Product;

namespace usecases.Cart;

public record AddToCartCommand(List<CartProductResponse> Cart) : ICommand<Result<CartResponse>>;

public class AddToCartHandler
    (IRepository<ProductEntity> repository, IRepository<CartEntity> cartRepository) : ICommandHandler<AddToCartCommand,
        Result<CartResponse>>
{
    public async Task<Result<CartResponse>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var selected = await repository.ListAsync(new GetProductByGuid(request.Cart.Select(x => x.ProductId).ToList()),
            cancellationToken);

        var products = new List<CartProductEntity>();

        foreach (var product in selected)
        {
            var selectedCart = request.Cart.FirstOrDefault(x => x.ProductId == product.Id);

            if (selectedCart!.Total < product.Stock)
            {
                continue;
            }
            
            products.Add(new CartProductEntity
            {
                ProductId = product.Id,
                Total = selectedCart.Total
            });
        }

        var entity = new CartEntity
        {
            Products = products
        };

        var result = await cartRepository.AddAsync(entity, cancellationToken);

        var dto = new CartResponse
        {
            Id = result.Id,
            List = result.Products.Select(x => new CartProductResponse
            {
                ProductId = x.ProductId,
                Total = x.Total
            }).ToList()
        };

        return Result.Success(dto);
    }
}