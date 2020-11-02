using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Mediator.Api.Database;
using Sample.Mediator.Api.Database.Entities;
using Sample.Mediator.Api.Models.Product;
using SimpleSoft.Mediator;

namespace Sample.Mediator.Api.Handlers.Product
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(ApiDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CreateProductResponse> HandleAsync(CreateProductCommand cmd, CancellationToken ct)
        {
            var products = _context.Set<ProductEntity>();

            if (await products.AnyAsync(p => p.Code == cmd.Code, ct))
            {
                throw new InvalidOperationException($"Product code '{cmd.Code}' already exists");
            }

            var externalId = Guid.NewGuid();
            await products.AddAsync(new ProductEntity
            {
                ExternalId = externalId,
                Code = cmd.Code,
                Name = cmd.Name,
                Price = cmd.Price
            }, ct);

            await _context.SaveChangesAsync(ct);

            await _mediator.BroadcastAsync(new CreatedProductEvent
            {
                ExternalId = externalId,
                Code = cmd.Code,
                Name = cmd.Name,
                Price = cmd.Price
            }, ct);

            return new CreateProductResponse
            {
                Id = externalId
            };
        }
    }

    public class CreateProductCommand : Command<CreateProductResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}