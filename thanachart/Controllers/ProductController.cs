using MediatR;
using Microsoft.AspNetCore.Mvc;
using usecases.Product;

namespace thanachart.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductList resource)
    {
        var result = await mediator.Send(new GetProductCommand(resource.Keyword, resource.Page, resource.PageSize));
        return Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetByIdProductCommand(id));
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductResource resource)
    {
        var result =
            await mediator.Send(new CreateProductCommand(resource.Name, resource.Sku, resource.Price, resource.Stock));
        return Ok(result);
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductResource resource)
    {
        var result = await mediator.Send(new UpdateProductCommand(id, resource.Name, resource.Sku, resource.Price,
            resource.Stock));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        return Ok(result);
    }


    public record GetProductList(string Keyword,
        int Page,
        int PageSize);

    public record ProductResource(string Name, string Sku, decimal Price, int Stock);
}