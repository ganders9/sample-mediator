using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Mediator.Api.Handlers.Product;
using Sample.Mediator.Api.Models.Product;
using SimpleSoft.Mediator;

namespace Sample.Mediator.Api.Controllers.Products
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> SearchAsync([FromQuery] string filterQ, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ProductModel> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetProductByIdQuery
            {
                ProductId = id
            }, ct);

            return result;
        }

        [HttpPost]
        public async Task<CreateProductResponse> CreateAsync([FromBody] CreateProductRequest model, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new CreateProductCommand
            {
                Code = model.Code,
                Name = model.Name,
                Price = model.Price
            }, ct);

            return result;
        }

        [HttpPut("{id:guid}")]
        public async Task UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProductRequest model, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}