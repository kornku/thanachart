using Ardalis.Result;
using Ardalis.SharedKernel;
using core.CartAggregate;
using core.CartAggregate.Specifications;
using core.ProductAggregate;
using core.ProductAggregate.Specifications;

namespace usecases.Cart;

public record CheckoutCommand(Guid id) : ICommand<Result<CheckoutResponse>>;

public class CheckoutResponse
{
    public decimal Price { get; set; }
    public Guid Id { get; set; }
}

public class CheckoutHandler
(IRepository<CartEntity> cartRepository,
    IRepository<ProductEntity> productRepository) : ICommandHandler<CheckoutCommand,
    Result<CheckoutResponse>>
{
    public async Task<Result<CheckoutResponse>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var selected = await cartRepository.FirstOrDefaultAsync(new GetCartByIdSpec(request.id),
            cancellationToken);

        if (selected == null)
        {
            return Result.Error("id not exist");
        }

        var listOfProduct =
            await productRepository.ListAsync(new GetProductByGuid(selected.Products.Select(x => x.Id).ToList()));

        decimal price = 0;

        foreach (var product in listOfProduct)
        {
            var cartProduct = selected.Products.FirstOrDefault(x => x.Id == product.Id);

            price += cartProduct!.Total * product.Price;

            var stock = product.Stock;

            stock = stock - cartProduct.Total;

            product.Stock = stock;

            await productRepository.UpdateAsync(product, cancellationToken);
        }

        var result = new CheckoutResponse
        {
            Price = price,
            Id = request.id,
        };

        return Result.Success(result);
    }
}