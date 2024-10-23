using MediatR;
using Microsoft.AspNetCore.Mvc;
using usecases.Cart;

namespace thanachart.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCartByIdCommand(id));
        return Ok(result);
    }

    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] CreateResource resource)
    {
        var result = await mediator.Send(new AddToCartCommand(resource.List));
        return Ok(result);
    }

    [HttpPost("{id:guid}/decrease")]
    public async Task<IActionResult> Decrease(Guid id, [FromBody] ChangeResource resource)
    {
        var result = await mediator.Send(new DecreaseCartCommand(id, resource.List));
        return Ok(result);
    }

    [HttpPost("{id:guid}/increase")]
    public async Task<IActionResult> Increase(Guid id, [FromBody] ChangeResource resource)
    {
        var result = await mediator.Send(new IncreaseCartCommand(id, resource.List));
        return Ok(result);
    }

    [HttpPost("{id:guid}/clear")]
    public async Task<IActionResult> Clear(Guid id)
    {
        var result = await mediator.Send(new ClearCartCommand(id));
        return Ok(result);
    }

    [HttpPost("{id:guid}/check_out")]
    public async Task<IActionResult> Checkout(Guid id)
    {
        var result = await mediator.Send(new CheckoutCommand(id));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCartCommand(id));
        return Ok(result);
    }

    public record CreateResource(List<CartProductResponse> List);

    public record ChangeResource(List<CartProductResponse> List);
}